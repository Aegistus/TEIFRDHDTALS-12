using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public string Description { get; }
    void StartInteract(GameObject interactor);
    void Interact(GameObject interactor);
    void StopInteract(GameObject interactor);
}
