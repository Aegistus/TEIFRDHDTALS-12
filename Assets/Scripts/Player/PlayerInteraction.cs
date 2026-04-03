using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<bool, string> OnInteractStateChange;

    [SerializeField] Transform raycastOrigin;
    public LayerMask interactableLayers;
    public float interactionDistance = 1f;
    int openDoorSoundID;

    PlayerController playerController;
    AgentEquipment agentEquipment;
    Cow currentlyMilking;
    IInteractable currentlyInteractingWith;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
        agentEquipment = GetComponent<AgentEquipment>();
    }

    RaycastHit rayHit;
    void FixedUpdate()
    {
        if (playerController.PauseInput)
        {
            return;
        }
        if (currentlyInteractingWith != null)
        {
            currentlyInteractingWith.Interact(gameObject);
        }
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out rayHit, interactionDistance, interactableLayers))
        {
            IInteractable interactable = rayHit.collider.GetComponentInParent<IInteractable>();
            if (Input.GetKey(KeyCode.E))
            {
                if (currentlyInteractingWith == null)
                {
                    if (interactable != null)
                    {
                        currentlyInteractingWith = interactable;
                        //currentlyInteractingWith.Interact(gameObject);
                    }
                }
                else
                {
                    currentlyInteractingWith.Interact(gameObject);
                }
                if (interactable != null)
                {
                    interactable.Interact(gameObject);
                    if (interactable is Cow)
                    {
                        currentlyMilking = (Cow)interactable;
                    }
                }
                else
                {
                    print("No interactable");
                }
            }
            //if (Input.GetKey(KeyCode.E))
            //{
            //    if (currentlyMilking != null)
            //    {
            //        currentlyMilking.Milk();
            //    }
            //    OnInteractStateChange?.Invoke(false, "");
            //}
            if (interactable != null)
            {
                OnInteractStateChange?.Invoke(true, interactable.Description);
            }

            //print("In interaction range");
        }
        else
        {
            OnInteractStateChange?.Invoke(false, "");
            currentlyMilking = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            currentlyInteractingWith = null;
        }
    }

}
