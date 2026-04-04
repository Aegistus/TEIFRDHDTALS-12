using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InGameDialogue dialogue;
    [SerializeField] Camera dialogueCamera;
    public string dialogueMusicID = "Dialogue_Theme";
    public UltEvent OnDialogueComplete;
    public UltEvent OnDialogueComplete02;

    public string Description => "Talk to NPC";

    int dialogueCompleteEvent = 0;
    GameObject mainCam;

    public void Interact(GameObject interactor)
    {
        InGameDialogueUI.Instance.OpenMenu(dialogue, this);
        if (Camera.main != null)
        {
            mainCam = Camera.main.gameObject;
            mainCam.SetActive(false);
        }
        dialogueCamera.gameObject.SetActive(true);
    }

    public void CompleteDialogue(bool aborted)
    {
        mainCam.SetActive(true);
        dialogueCamera.gameObject.SetActive(false);
        if (!aborted)
        {
            if (dialogueCompleteEvent == 0)
            {
                OnDialogueComplete.Invoke();
                dialogueCompleteEvent++;
            }
            else if (dialogueCompleteEvent == 1)
            {
                OnDialogueComplete02.Invoke();
                dialogueCompleteEvent++;
            }
        }
    }

    public void ChangeDialogue(InGameDialogue dialogue)
    {
        this.dialogue = dialogue;
    }

    public void StopInteract(GameObject interactor)
    {
        //throw new System.NotImplementedException();
    }
}
