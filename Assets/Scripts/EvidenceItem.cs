using UnityEngine;
using UnityEngine.UI;

public class EvidenceItem : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private string nameText;
    [SerializeField] private string descriptionText;

    [SerializeField] private NotebookUIManager notebookUIManager;

    private bool _isChecked = false;

    [SerializeField] private Sprite defaultIcon;
    // [SerializeField] private Sprite crowbarIcon;
    // [SerializeField] private Sprite fakeDocumentIcon;
    // ...

    private Button button;

    [System.Obsolete]
    private void Awake()
    {
        // Получаем кнопку и настраиваем обработчик клика
        button = GetComponent<Button>();
        button.onClick.AddListener(OnEvidenceClicked);
    }

    [System.Obsolete]
    private void OnEvidenceClicked()
    {
        if (!_isChecked)
        {
            NotebookUIManager manager = FindObjectOfType<NotebookUIManager>();
            manager.AddEvidence(nameText, descriptionText);
            _isChecked = true;       
        }
    }


    public void Initialize(string evidenceName)
    {
        nameText = evidenceName;
        descriptionText = GetDescription(evidenceName);
        iconImage.sprite = GetIcon(evidenceName);
    }

    private Sprite GetIcon(string evidenceName)
    {
        return evidenceName switch
        {
            // "Отмычка" => crowbarIcon,
            // "Поддельный документ" => fakeDocumentIcon,
            _ => defaultIcon 
        };
    }

    private string GetDescription(string evidenceName)
    {
        return evidenceName switch
        {
            "Отмычка" => "Инструмент для вскрытия замков",
            "Поддельный документ" => "Фальшивый паспорт с печатями",
            _ => "Улика, связанная с преступлением"
        };
    }
}