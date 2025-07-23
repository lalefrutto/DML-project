using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DocumentStamping : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject redStampPrefab; 
    [SerializeField] private GameObject greenStampPrefab;
    [SerializeField] private Button redButton;
    [SerializeField] private Button greenButton; 
    [SerializeField] private GameObject continueButton;

    private bool isOverDocument = false; 
    private bool canStamp = true; 
    private bool isRedStampSelected = false; 
    private bool isGreenStampSelected = false; 

    private RectTransform documentRectTransform;

   
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOverDocument = true;
    }

   
    public void OnPointerExit(PointerEventData eventData)
    {
        isOverDocument = false;
    }

  
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOverDocument && canStamp)
        {
            SpawnStamp(eventData.position); 
        }
    }

    private void SpawnStamp(Vector2 clickPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(documentRectTransform, clickPosition, null, out localPoint);

        // Проверка, что штамп не выходит за пределы документа
        if (!documentRectTransform.rect.Contains(localPoint))
        {
            return; 
        }

       
        GameObject stampPrefab = isRedStampSelected ? redStampPrefab : greenStampPrefab;
        GameObject stamp = Instantiate(stampPrefab, documentRectTransform);
        stamp.transform.localPosition = localPoint; 

        
        if (!isRedStampSelected || !isGreenStampSelected)
        {
            redButton.gameObject.SetActive(false);
            greenButton.gameObject.SetActive(false);
        }

        
        canStamp = false;
        continueButton.SetActive(true);
    }

    
    public void SelectRedStamp()
    {
        isRedStampSelected = true;
    }

   
    public void SelectGreenStamp()
    {
        isGreenStampSelected = true;
    }

    void Start()
    {
        
        documentRectTransform = GetComponent<RectTransform>();

       
        redButton.onClick.AddListener(SelectRedStamp);
        greenButton.onClick.AddListener(SelectGreenStamp);
    }
}
