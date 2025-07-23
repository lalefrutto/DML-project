using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDayPanelUI : MonoBehaviour
{
    // [Header("UI Elements")]
    // [SerializeField] private TextMeshProUGUI salaryText;       // Зарплата
    // [SerializeField] private TextMeshProUGUI savingsText;      // Накопления
    // [SerializeField] private TextMeshProUGUI expensesText;     // Затраты
    // [SerializeField] private TextMeshProUGUI familyStatusText; // Состояние семьи
    // [SerializeField] private Button okButton;                  // Кнопка OK

    // Для примера, создадим простые переменные для данных
    private int salary = 1000;
    private int savings = 5000;
    private int expenses = 800;
    private string familyStatus = "Семья в порядке";

    void Start()
    {
        // Устанавливаем начальные значения
        UpdateUI();

        // Добавляем обработчик на кнопку OK
        // okButton.onClick.AddListener(OnOkButtonPressed);
    }

    // Метод обновления UI
    void UpdateUI()
    {
        // salaryText.text = "Зарплата: " + salary.ToString() + " руб.";
        // savingsText.text = "Накопления: " + savings.ToString() + " руб.";
        // expensesText.text = "Затраты: " + expenses.ToString() + " руб.";
        // familyStatusText.text = "Состояние семьи: " + familyStatus;
        return;
    }

    // Метод для обработки нажатия кнопки OK
    void OnOkButtonPressed()
    {
        // Закрыть панель или переход к следующему этапу
        gameObject.SetActive(false); // Скрыть панель, например
    }
}
