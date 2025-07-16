using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionDescriptor : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private TMP_Text actionText;

    private static ActionDescriptor instance;

    private void Awake()
    {
        instance = this;
        HideAction();
    }

    public static void ShowAction(string text)
    {
        instance.actionText.text = text;
        instance.actionPanel.SetActive(true);
    }

    public static void HideAction()
    {
        instance.actionPanel.SetActive(false);
    }
}