using UnityEngine;

public class WitnessDialogueActivate : Interactable
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private NotebookUIManager notebookUIManager;

    private bool _isInterviewed = false;  

    protected override string ActionDescription => "Опросить свидетеля";

    private void OnMouseDown()
    {
        if (dialoguePanel != null & !_isInterviewed)
        {
            DialogueTextFlow dialogueTextFlow = dialoguePanel.GetComponent<DialogueTextFlow>();
            dialogueTextFlow.StartDialogue();
            if (!_isInterviewed)
            {
                notebookUIManager.AddTestimony(GameManager.Instance.GetWitnessTestimony());
                _isInterviewed = true;
            }
            
        }
    }
}