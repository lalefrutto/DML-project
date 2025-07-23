using UnityEngine;
using System.Collections.Generic;

public class EvidenceBox : MonoBehaviour
{
    [SerializeField] private RectTransform evidencePanel;
    [SerializeField] private GameObject evidenceItemPrefab;



    public List<string> EvidenceItems = new List<string>();

    public void AddEvidence(string evidenceName)
    {
        EvidenceItems.Add(evidenceName);

        GameObject evidenceItem = Instantiate(
            evidenceItemPrefab,
            evidencePanel
        );
        
        evidenceItem.GetComponent<EvidenceItem>().Initialize(evidenceName);
        SetRandomPosition(evidenceItem.GetComponent<RectTransform>());

    }

    private void SetRandomPosition(RectTransform itemRect)
    {
        Vector2 panelSize = evidencePanel.rect.size;
        Vector2 itemSize = itemRect.rect.size;

        float randomX = Random.Range(
            itemSize.x / 2,
            panelSize.x - itemSize.x / 2
        );
        float randomY = Random.Range(
            itemSize.y / 2,
            panelSize.y - itemSize.y / 2
        );

        itemRect.anchoredPosition = new Vector2(randomX, randomY);
    }

    public void ClearEvidence()
    {
        foreach (Transform child in evidencePanel)
        {
            Destroy(child.gameObject);
        }
    }


}
