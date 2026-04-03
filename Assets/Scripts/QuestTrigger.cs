using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class QuestTrigger : MonoBehaviour
{
    public UltEvent OnPlayerEnter;

    bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered)
        {
            return;
        }
        if (other.GetComponent<PlayerController>() != null || other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
            alreadyTriggered = true;
        }
    }

}
