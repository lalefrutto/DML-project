using System;
using System.Collections.Generic;
using UnityEngine;

public class WitnessManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> witnessButtons;
    [SerializeField] private DialogueTextFlow dialogueTextFlow;

    private int currentWitnessIndex;
    private int maxWitnessIndex;

    public event Action OnWitnessChanged;


    private void UpdateMaxWitnessIndex()
    {
        if (GameManager.Instance != null)
        {
            maxWitnessIndex = Mathf.Min(
                witnessButtons.Count,
                GameManager.Instance.getWitnessCount()
            );
        }
        else
        {
            maxWitnessIndex = witnessButtons.Count;
        }
    }

    public int GetCurrentWitnessIndex()
    {
        return currentWitnessIndex;
    }

    private void Start()
    {
        currentWitnessIndex = 0;
        UpdateMaxWitnessIndex();
        ShowCurrentWitness();
    }

    public void AdvanceToNextWitness()
    {
        UpdateMaxWitnessIndex();
        currentWitnessIndex++;

        //witness dialogue method that gives text to next dialogue

        if (currentWitnessIndex >= maxWitnessIndex)
        {
            return;
        }



        ShowCurrentWitness();

        dialogueTextFlow.ResetDialogue();
        OnWitnessChanged?.Invoke();
    }

    private void ShowCurrentWitness()
    {
        for (int i = 0; i < witnessButtons.Count; i++)
        {
            bool shouldShow = (i == currentWitnessIndex) && (i < maxWitnessIndex);
            witnessButtons[i].SetActive(shouldShow);
        }
    }
}
