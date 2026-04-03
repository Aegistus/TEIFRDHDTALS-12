using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBookUI : MonoBehaviour
{
    [SerializeField] List<TMP_Text> questTitles;
    [SerializeField] TMP_Text currentQuestTitle;
    [SerializeField] TMP_Text currentQuestDescription;
    [SerializeField] Transform questObjectiveParent;
    [SerializeField] GameObject questObjectivePrefab;

    public bool QuestBookOpen { get; private set; }

    QuestManager questManager;
    MapUI map;

    private void Start()
    {
        questManager = QuestManager.Instance;
        CloseBook();
        questManager.OnActiveQuestChanged += QuestManager_OnActiveQuestChanged;
        map = FindAnyObjectByType<MapUI>();
    }

    private void QuestManager_OnActiveQuestChanged(Quest obj)
    {
        UpdateBook();
    }

    private void Update()
    {
        if (map.MapOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (QuestBookOpen)
            {
                CloseBook();
            }
            else
            {
                OpenBook();
            }
        }
        if (QuestBookOpen && Input.GetKeyDown(KeyCode.Alpha0))
        {
            questManager.ChangeCurrentFocusedQuest();
        }
    }

    public void OpenBook()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        UpdateBook();
        QuestBookOpen = true;
        Time.timeScale = 0;
    }

    public void CloseBook()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        QuestBookOpen = false;
        Time.timeScale = 1;
    }

    public void UpdateBook()
    {
        if (questManager.activeQuests.Count == 0)
        {
            return;
        }
        for (int i = 0; i < questTitles.Count; i++)
        {
            if (i < questManager.activeQuests.Count)
            {
                questTitles[i].text = questManager.activeQuests[i].title;
            }
            else
            {
                questTitles[i].text = string.Empty;
            }
        }
        currentQuestTitle.text = questManager.currentQuest.title;
        currentQuestDescription.text = questManager.currentQuest.description;
        for (int i = 0; i < questObjectiveParent.childCount; i++)
        {
            Destroy(questObjectiveParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < questManager.currentQuest.unlockedObjectives.Count; i++)
        {
            var objectiveUI = Instantiate(questObjectivePrefab, questObjectiveParent);
            objectiveUI.GetComponent<TMP_Text>().text = questManager.currentQuest.unlockedObjectives[i].description;
        }
    }
}
