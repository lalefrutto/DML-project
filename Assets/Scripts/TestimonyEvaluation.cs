using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TestimonyType = CaseGenerator.TestimonyType;

public class TestimonyEvaluation : MonoBehaviour
{
    [SerializeField] PlayerCheck playerCheck;

    [SerializeField] private Button trueButton;
    [SerializeField] private Button falseButton;
    [SerializeField] private Button neutralButton;

    [SerializeField] private Color selectedColor = Color.white; // Цвет кнопки при выборе
    [SerializeField] private Color defaultColor = Color.white; // Цвет по умолчанию для кнопки
    [SerializeField] private Color disabledColor = Color.gray; // Цвет для невыбранных кнопок

    private TestimonyType currentEvaluation;
    private Button lastSelectedButton;

    void Start()
    {
        trueButton.onClick.AddListener(() => EvaluateTestimony(TestimonyType.Правдивое, trueButton));
        falseButton.onClick.AddListener(() => EvaluateTestimony(TestimonyType.Ложное, falseButton));
        neutralButton.onClick.AddListener(() => EvaluateTestimony(TestimonyType.Нейтральное, neutralButton));
    }

    void EvaluateTestimony(TestimonyType evaluation, Button selectedButton)
    {
        // Сбрасываем цвет всех кнопок в стандартный
        trueButton.GetComponent<Image>().color = defaultColor;
        falseButton.GetComponent<Image>().color = defaultColor;
        neutralButton.GetComponent<Image>().color = defaultColor;

        // Меняем цвет на серый для невыбранных
        if (selectedButton != trueButton)
            trueButton.GetComponent<Image>().color = disabledColor;
        if (selectedButton != falseButton)
            falseButton.GetComponent<Image>().color = disabledColor;
        if (selectedButton != neutralButton)
            neutralButton.GetComponent<Image>().color = disabledColor;

        // Выбираем цвет для выбранной кнопки
        selectedButton.GetComponent<Image>().color = selectedColor;

        // Обновляем текущую оценку
        currentEvaluation = evaluation;

        // playerCheck.AddTestimonyType(currentEvaluation);

        // Запоминаем последнюю выбранную кнопку
        lastSelectedButton = selectedButton;
    }
}
