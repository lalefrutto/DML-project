using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public RectTransform documentImage;
    public TextMeshProUGUI introText;
    public GameObject StartText;
    public Image panelImage;
    public GameObject FullPanel;

    [TextArea(3, 10)]
    public string[] introLines;

    public float documentSlideSpeed = 300f;
    public float typingSpeed = 0.03f;

    public Vector2 stopPosition = new Vector2(0, 474);

    private void Start()
    {
        StartCoroutine(WaitForPlayerInput());
    }

    IEnumerator WaitForPlayerInput()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        StartText.SetActive(false);
        yield return StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        Vector2 targetPosition = stopPosition;
        while (Vector2.Distance(documentImage.anchoredPosition, targetPosition) > 1f)
        {
            documentImage.anchoredPosition = Vector2.MoveTowards(
                documentImage.anchoredPosition,
                targetPosition,
                documentSlideSpeed * Time.deltaTime
            );
            yield return null;
        }


        foreach (string line in introLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(1f);
        }

        yield return StartCoroutine(FadeOutPanel());

        // false
    }

    IEnumerator TypeLine(string line)
    {
        introText.text = "";
        foreach (char c in line)
        {
            introText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator FadeOutPanel()
    {
        float fadeDuration = 1f;
        float timeElapsed = 0f;

        // Получаем все дочерние элементы с компонентами Image и TextMeshProUGUI
        Image[] childImages = panelImage.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] childTexts = panelImage.GetComponentsInChildren<TextMeshProUGUI>();

        // Сохраняем начальные цвета
        Color panelColor = panelImage.color;
        Color[] childImageColors = new Color[childImages.Length];
        Color[] childTextColors = new Color[childTexts.Length];

        for (int i = 0; i < childImages.Length; i++)
        {
            childImageColors[i] = childImages[i].color;
        }

        for (int i = 0; i < childTexts.Length; i++)
        {
            childTextColors[i] = childTexts[i].color;
        }

        while (timeElapsed < fadeDuration)
        {
            float alphaValue = 1f - (timeElapsed / fadeDuration);

            // Применяем прозрачность к основной панели
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, alphaValue);

            // Применяем прозрачность ко всем дочерним изображениям
            for (int i = 0; i < childImages.Length; i++)
            {
                childImages[i].color = new Color(
                    childImageColors[i].r,
                    childImageColors[i].g,
                    childImageColors[i].b,
                    alphaValue
                );
            }

            // Применяем прозрачность ко всем дочерним текстам
            for (int i = 0; i < childTexts.Length; i++)
            {
                childTexts[i].color = new Color(
                    childTextColors[i].r,
                    childTextColors[i].g,
                    childTextColors[i].b,
                    alphaValue
                );
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем окончательные прозрачные цвета
        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0f);

        for (int i = 0; i < childImages.Length; i++)
        {
            childImages[i].color = new Color(
                childImageColors[i].r,
                childImageColors[i].g,
                childImageColors[i].b,
                0f
            );
        }

        for (int i = 0; i < childTexts.Length; i++)
        {
            childTexts[i].color = new Color(
                childTextColors[i].r,
                childTextColors[i].g,
                childTextColors[i].b,
                0f
            );
        }

        StartText.SetActive(false);
        FullPanel.SetActive(false);
}

}
