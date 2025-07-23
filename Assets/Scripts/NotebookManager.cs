using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotebookUIManager : MonoBehaviour
{
    [Header("Notebook Panels")]
    public GameObject notebookPanel;
    public GameObject evidencePanel;
    public GameObject testimonyPanel;

    [Header("Buttons")]
    public Button openNotebookButton;
    public Button closeNotebookButton;
    public Button evidenceTabButton;
    public Button testimonyTabButton;

    [Header("Content Containers")]
    public Transform evidenceListContent;
    public Transform testimonyListContent;

    [Header("Prefabs")]
    public GameObject evidenceItemPrefab;
    public GameObject testimonyItemPrefab;
    
    // [SerializeField] private PlayerCheck playerCheck; 

    private void Start()
    {
        openNotebookButton.onClick.AddListener(OpenNotebook);
        closeNotebookButton.onClick.AddListener(CloseNotebook);
        evidenceTabButton.onClick.AddListener(ShowEvidencePanel);
        testimonyTabButton.onClick.AddListener(ShowTestimonyPanel);

        CloseNotebook();
    }

    public void OpenNotebook()
    {
        notebookPanel.SetActive(true);
        ShowEvidencePanel(); 
    }

    public void CloseNotebook()
    {
        notebookPanel.SetActive(false);
    }

    public void ShowEvidencePanel()
    {
        evidencePanel.SetActive(true);
        testimonyPanel.SetActive(false);
    }

    public void ShowTestimonyPanel()
    {
        testimonyPanel.SetActive(true);
        evidencePanel.SetActive(false);
    }

    public void AddEvidence(string evidenceName, string evidenceDescription)
    {
        GameObject item = Instantiate(evidenceItemPrefab, evidenceListContent);
        item.GetComponentInChildren<TMP_Text>().text = $"{evidenceName}\n{evidenceDescription}";
    }

    public void AddTestimony(CaseGenerator.WitnessTestimony testimony)
    {
        if (testimony != null && testimony.Description != null)
        {
            GameObject item = Instantiate(testimonyItemPrefab, testimonyListContent);

            TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = testimony.Description;
            }
            else
            {
                Debug.LogWarning("TMP_Text component not found in the testimony item prefab.");
            }

            // PlayerCheck.Instance.AddTestimonyType(testimony.Type); (Если необходимо)
        }
        else
        {
            Debug.LogWarning("Testimony or its description is null.");
        }
        // PlayerCheck.Instance.AddTestimonyType(testimony.Type);
    }
}
