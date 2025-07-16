using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RenderVerdictButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActionDescriptor.ShowAction("Вынести вердикт (в разработке)");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActionDescriptor.HideAction();
    }  
}
