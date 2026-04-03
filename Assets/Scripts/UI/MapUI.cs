using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] GameObject menu;

    public bool MapOpen { get; private set; } = false;

    QuestBookUI questUI;

    private void Start()
    {
        menu.SetActive(false);
        questUI = FindAnyObjectByType<QuestBookUI>();
    }

    private void Update()
    {
        if (questUI.QuestBookOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (MapOpen)
            {
                menu.SetActive(false);
                MapOpen = false;
            }
            else
            {
                menu.SetActive(true);
                MapOpen = true;
            }
        }
    }
}
