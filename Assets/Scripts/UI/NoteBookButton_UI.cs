using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteBookButton_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float normalAlpha = 0.5f; 
    [SerializeField] private float hoverAlpha = 1f;    

    private Image _buttonImage;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        SetAlpha(normalAlpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAlpha(hoverAlpha);
        ActionDescriptor.ShowAction("Открыть папку (в разработке)");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(normalAlpha);
        ActionDescriptor.HideAction();
    }

    private void SetAlpha(float alpha)
    {
        Color color = _buttonImage.color;
        color.a = alpha;
        _buttonImage.color = color;
    }
}