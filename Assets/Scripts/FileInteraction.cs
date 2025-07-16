using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileInteraction : Interactable 
{
    private Button button;
    [SerializeField] private GameObject FullScreenFile;
    [SerializeField] private GameObject FilePanel;
    [SerializeField] private Button closeFullScreenButton;



    protected override string ActionDescription => "Изучить досье";


    void Start()
    {
        closeFullScreenButton = FullScreenFile.GetComponentInChildren<Button>();
        closeFullScreenButton.onClick.AddListener(CloseFullScreen);
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenFullScreen);
    }

    private void OpenFullScreen()
    {
        GameManager.Instance.SetTrialCanStart(true);
        gameObject.SetActive(false);
        FullScreenFile.SetActive(true);
        FilePanel.SetActive(true);
    }

    private void CloseFullScreen()
    {
        FilePanel.SetActive(false);
        FullScreenFile.SetActive(false); 
        gameObject.SetActive(true); 
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OpenFullScreen);
        closeFullScreenButton.onClick.RemoveListener(CloseFullScreen);
    }

}