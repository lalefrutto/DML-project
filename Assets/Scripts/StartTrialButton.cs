using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class StartTrialButton : Interactable
{
    // [Header("Sprites")]
    // [SerializeField] private Sprite activeSprite;    
    // [SerializeField] private Sprite inactiveSprite;  

    protected override string ActionDescription => "Начать суд";

    [SerializeField] GameObject PretrialScene;
    [SerializeField] GameObject TrialScene;
    [SerializeField] GameObject RenderVerdictButton;

    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.red;

    private Image _buttonImage;
    private Button _button;

    void Start()
    {
        _buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(StartTrial);

        UpdateButtonState(GameManager.Instance.GetTrialCanStart());

    }

    private void UpdateButtonState(bool isActive)
    {
        _buttonImage.color = isActive ? activeColor : inactiveColor;

        _button.interactable = isActive;
    }

    public void StartTrial()
    {
        PretrialScene.SetActive(false);
        TrialScene.SetActive(true);
        RenderVerdictButton.SetActive(true);

        gameObject.SetActive(false);
    }

    public void Activate()
    {
        UpdateButtonState(true);
    }

    void OnDestroy()
    {
        _button.onClick.RemoveListener(StartTrial);
    }
}