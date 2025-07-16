using UnityEngine;
using System.Collections.Generic;

public class EvidenceBox : MonoBehaviour
{
    public List<string> EvidenceItems = new List<string>();

    public void AddEvidence(string evidence)
    {
        EvidenceItems.Add(evidence);
        Debug.Log("Добавлена улика: " + evidence); 
    }

}
