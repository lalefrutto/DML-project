using UnityEngine;

public class WitnessDialogueActivate : Interactable
{
    [SerializeField] private GameObject dialoguePanel;


    protected override string ActionDescription => "Опросить свидетеля";

    private void OnMouseDown()
    {
        if (dialoguePanel != null)
        {
            DialogueTextFlow dialogueTextFlow = dialoguePanel.GetComponent<DialogueTextFlow>();
            dialogueTextFlow.StartDialogue();
        }
    }
}