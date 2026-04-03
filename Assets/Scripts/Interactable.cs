using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string description;
    [SerializeField] UltEvent OnInteract;
    [SerializeField] UltEvent OnStopInteract;

    public string Description => description;

    public void Interact(GameObject interactor)
    {
        OnInteract?.Invoke();
    }

    public void StopInteract(GameObject interactor)
    {
        OnStopInteract?.Invoke();
    }
}
