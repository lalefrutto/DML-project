using UnityEngine;

public class WitnessDialogueActivate : Interactable
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private NotebookUIManager notebookUIManager;
    [SerializeField] private WitnessManager witnessManager;

    private bool _isInterviewed = false;  

    protected override string ActionDescription => "Опросить свидетеля";
    
    private void Start()
    {
        DialogueTextFlow dialogueTextFlow = dialoguePanel.GetComponent<DialogueTextFlow>();
        dialogueTextFlow.OnDialogueEnded += HandleDialogueEnd;
    }

    private void HandleDialogueEnd()
    {
        witnessManager.AdvanceToNextWitness();
    }

    private void OnMouseDown()
    {
        if (dialoguePanel != null & !_isInterviewed)
        {
            DialogueTextFlow dialogueTextFlow = dialoguePanel.GetComponent<DialogueTextFlow>();
            dialogueTextFlow.StartDialogue();
            if (!_isInterviewed)
            {
                notebookUIManager.AddTestimony(GameManager.Instance.GetWitnessTestimonyFrom(witnessManager.GetCurrentWitnessIndex()));
                _isInterviewed = true;
            }

        }
    }
}