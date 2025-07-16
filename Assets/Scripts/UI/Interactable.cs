using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected virtual string ActionDescription => "Взаимодействовать";

    public void OnPointerEnter(PointerEventData eventData) => 
        ActionDescriptor.ShowAction(ActionDescription);

    public void OnPointerExit(PointerEventData eventData) => 
        ActionDescriptor.HideAction();
}