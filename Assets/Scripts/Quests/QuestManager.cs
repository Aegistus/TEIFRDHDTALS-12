using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestEnum
{
    MainQuest, DesertCourier, FetchQuest, KilotonKatastrophe
}

public class QuestManager : MonoBehaviour
{
    public event Action<Quest> OnActiveQuestChanged;

    public static QuestManager Instance { get; private set; }

    public List<Quest> activeQuests;
    public Quest currentQuest;

    [SerializeField] float questUpdateDelay = 4f;
    [SerializeField] float startQuestDelay = 3f;
    [SerializeField] Quest mainQuest;
    [SerializeField] List<Quest> sideQuests;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddQuestWithDelay(QuestEnum.MainQuest, 3);
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        currentQuest = quest;
        QuestPopup.Instance.ShowQuestStartPopup(quest.title);
        quest.currentObjective = quest.objectives[0];
        UpdateQuestObjective(quest.questEnum, 0);
    }

    public void AddQuest(QuestEnum questEnum)
    {
        Quest quest = sideQuests.Find(q => q.questEnum == questEnum);
        quest ??= mainQuest;
        AddQuest(quest);
    }

    public void AddQuestWithDelay(QuestEnum questEnum, float delay)
    {
        StartCoroutine(QuestDelay(questEnum, delay));
    }

    IEnumerator QuestDelay(QuestEnum questEnum, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddQuest(questEnum);
    }

    public void UpdateQuestObjective(QuestEnum questEnum, int newObjectiveIndex)
    {
        Quest quest = activeQuests.Find(q => q.questEnum == questEnum);
        if (quest == null || quest.currentObjectiveInd >= newObjectiveIndex)
        {
            return;
        }
        quest.currentObjectiveInd = newObjectiveIndex;
        quest.currentObjective.OnFinish.Invoke();
        StartCoroutine(UpdateDelay(quest, newObjectiveIndex));
    }

    public void GoToNextQuestObjective(QuestEnum questEnum)
    {
        Quest quest = activeQuests.Find(q => q.questEnum == questEnum);
        if (quest == null)
        {
            return;
        }
        int newIndex = quest.currentObjectiveInd + 1;
        UpdateQuestObjective(questEnum, newIndex); 
    }

    public void FinishQuest(QuestEnum questEnum)
    {
        Quest quest = activeQuests.Find(q => q.questEnum == questEnum);
        quest.currentObjective?.OnFinish?.Invoke();
        if (quest == null)
        {
            return;
        }
        activeQuests.Remove(quest);
        QuestPopup.Instance.ShowQuestEndPopup(quest.title);
        quest.OnQuestComplete?.Invoke();
        if (quest == currentQuest)
        {
            ChangeCurrentFocusedQuest();
        }
    }

    IEnumerator UpdateDelay(Quest quest, int objectiveIndex)
    {
        yield return new WaitForSeconds(questUpdateDelay);
        if (currentQuest != quest)
        {
            ChangeCurrentFocusedQuest(quest);
        }
        quest.unlockedObjectives.Add(quest.objectives[objectiveIndex]);
        quest.currentObjective = quest.objectives[objectiveIndex];
        quest.currentObjective.OnStart.Invoke();
        QuestUpdatePopup.Instance.ShowPopup(quest.currentObjective.description);
        SoundManager.Instance.PlaySoundGlobal("Quest_New_Objective");
        var questMarker = GameObject.FindWithTag("Quest Marker");
        if (questMarker != null)
        {
            questMarker.transform.position = quest.currentObjective.location.position + quest.currentObjective.offset;
        }
    }

    public void ChangeCurrentFocusedQuest()
    {
        if (activeQuests.Count == 0) { return; }
        int index = activeQuests.FindIndex(q => q.questEnum == currentQuest.questEnum);
        index = (index + 1) % activeQuests.Count;
        currentQuest = activeQuests[index];
        var questMarker = GameObject.FindWithTag("Quest Marker");
        if (questMarker != null)
        {
            questMarker.transform.position = currentQuest.currentObjective.location.position + currentQuest.currentObjective.offset;
        }
        OnActiveQuestChanged?.Invoke(currentQuest);
    }

    public void ChangeCurrentFocusedQuest(Quest quest)
    {
        currentQuest = quest;
        var questMarker = GameObject.FindWithTag("Quest Marker");
        if (questMarker != null)
        {
            questMarker.transform.position = currentQuest.currentObjective.location.position + currentQuest.currentObjective.offset;
        }
        OnActiveQuestChanged?.Invoke(currentQuest);
    }
}
