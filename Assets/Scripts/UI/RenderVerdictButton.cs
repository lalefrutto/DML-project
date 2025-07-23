using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RenderVerdictButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("Sprite Settings")]
    public Sprite normalSprite;    
    public Sprite selectedSprite; 
    
    private Image buttonImage;     

     void Awake()
    {
        buttonImage = GetComponent<Image>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = selectedSprite;
        ActionDescriptor.ShowAction("Вынести вердикт");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        ActionDescriptor.HideAction();
    }  
}
