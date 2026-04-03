using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string description;
    [SerializeField] UltEvent OnInteract;

    public string Description => description;

    public void Interact(GameObject interactor)
    {
        OnInteract?.Invoke();
    }
}
