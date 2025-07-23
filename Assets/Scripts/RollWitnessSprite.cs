using UnityEngine;

public class RollWitnessSprite : MonoBehaviour
{
    [System.Serializable]
    public class WitnessSpritePair
    {
        public Sprite normalSprite;    // Основной спрайт
        public Sprite hoverSprite;     // Спрайт при наведении
    }

    [SerializeField] private WitnessSpritePair[] witnessSprites; // Массив пар спрайтов для каждого свидетеля
    private SpriteRenderer spriteRenderer;

    private int currentWitnessIndex = 0; // Индекс текущего спрайта

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer
        ChangeWitnessSprite(); // Меняем спрайт сразу при старте
    }

    // Функция для изменения спрайта свидетеля
    void ChangeWitnessSprite()
    {
        if (witnessSprites.Length > 0)
        {
            // Выбираем случайный индекс для спрайта
            currentWitnessIndex = Random.Range(0, witnessSprites.Length);

            // Устанавливаем спрайт по умолчанию
            spriteRenderer.sprite = witnessSprites[currentWitnessIndex].normalSprite;
        }
        else
        {
            Debug.LogWarning("No witness sprites assigned!");
        }
    }

    // Вызывается при наведении на свидетеля
    void OnMouseEnter()
    {
        if (witnessSprites.Length > 0)
        {
            spriteRenderer.sprite = witnessSprites[currentWitnessIndex].hoverSprite; // Меняем на спрайт при наведении
        }
    }

    // Вызывается, когда указатель мыши покидает область свидетеля
    void OnMouseExit()
    {
        if (witnessSprites.Length > 0)
        {
            spriteRenderer.sprite = witnessSprites[currentWitnessIndex].normalSprite; // Возвращаем исходный спрайт
        }
    }
}
