using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using System;

public class WitnessDialogue : MonoBehaviour
{

    [SerializeField] private CaseGenerator caseGenerator;
    [SerializeField] private DisplayDossier getDossier;
    [SerializeField] private DialogueTextFlow dialogueTextFlow;
    [SerializeField] private WitnessManager witnessManager;

    private Dictionary<PhraseType, List<string>> phraseTemplates = new Dictionary<PhraseType, List<string>>();


    public enum PhraseType
    {
        Opening,
        Between,
        Ending
    }


    private void InitializeDefaultPhrases()
    {
        phraseTemplates[PhraseType.Opening] = new List<string>
        {
            "Я хочу рассказать кое-что важное...",
            "В тот день я видел кое-что подозрительное...",
            "Позвольте мне рассказать, что произошло...",
            "Я помню этот день очень четко...",
            "У меня есть информация по этому делу..."
        };

        phraseTemplates[PhraseType.Between] = new List<string>
        {
            "И еще...",
            "Потом я заметил...",
            "Вдруг я увидел...",
            "Затем произошло нечто странное...",
            "После этого...",
            "Я также хотел бы добавить...",
            "Кроме того..."
        };

        phraseTemplates[PhraseType.Ending] = new List<string>
        {
            "Это все, что я могу сказать.",
            "Больше я ничего не видел.",
            "На этом мои показания заканчиваются.",
            "Я рассказал все, что знаю.",
            "Это все, что мне известно по данному вопросу.",
            "Если я вспомню что-то еще, я сообщу."
        };
    }

    // was Start
    void Awake()
    {
        // InitializeDefaultPhrases();
        // CaseGenerator.WitnessTestimony testimony = GameManager.Instance.GetWitnessTestimonyFrom(witnessManager.GetCurrentWitnessIndex());
        // giveWitnessTestimony(testimony);
        // give dialogue text flow the string

        InitializeDefaultPhrases();
        witnessManager.OnWitnessChanged += HandleWitnessChanged; // Подписка на событие
        InitializeForCurrentWitness(); // Инициализация для первого свидетеля
    }
    

    private void OnDestroy()
    {
        // Отписываемся при уничтожении объекта
        if (witnessManager != null)
        {
            witnessManager.OnWitnessChanged -= HandleWitnessChanged;
        }
    }

    private void HandleWitnessChanged()
    {
        InitializeForCurrentWitness();
    }

    public void InitializeForCurrentWitness()
    {
        Debug.Log(witnessManager.GetCurrentWitnessIndex());
        CaseGenerator.WitnessTestimony testimony = GameManager.Instance.GetWitnessTestimonyFrom(
            witnessManager.GetCurrentWitnessIndex()
        );
        giveWitnessTestimony(testimony);
    }


    #region Absolute Cinema

    private string GenerateFullTestimony(CaseGenerator.WitnessTestimony testimony)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(GetRandomPhrase(PhraseType.Opening)).Append(" ");

        string[] sentences = SplitIntoSentences(testimony.Description);
        if (sentences.Length == 0) return sb.ToString().Trim();

        int insertPoint = sentences.Length > 3 ? sentences.Length / 2 : -1;

        for (int i = 0; i < sentences.Length; i++)
        {
            sb.Append(sentences[i]);

            if (i == insertPoint)
            {
                sb.Append(" ").Append(GetRandomPhrase(PhraseType.Between)).Append(" ");
            }
            else if (i < sentences.Length - 1)
            {
                sb.Append(" ");
            }
        }

        sb.Append(" ").Append(GetRandomPhrase(PhraseType.Ending)).Append(" ");
        return sb.ToString();
    }



    private string GetRandomPhrase(PhraseType type)
    {
        if (phraseTemplates.ContainsKey(type) && phraseTemplates[type].Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, phraseTemplates[type].Count);
            return phraseTemplates[type][randomIndex];
        }
        return string.Empty;
    }

    private string[] SplitIntoSentences(string text)
    {
        List<string> sentences = new List<string>();
        int startIndex = 0;
        bool insideTag = false;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '<') insideTag = true;
            else if (text[i] == '>') insideTag = false;
            
            if (!insideTag && ".!?".Contains(text[i]))
            {
                if (i + 1 >= text.Length || char.IsWhiteSpace(text[i + 1]) || text[i + 1] == '\"')
                {
                    string sentence = text.Substring(startIndex, i - startIndex + 1).Trim();
                    if (!string.IsNullOrEmpty(sentence)) 
                        sentences.Add(sentence);
                    startIndex = i + 1;
                }
            }
        }

        if (startIndex < text.Length)
        {
            string lastSentence = text.Substring(startIndex).Trim();
            if (!string.IsNullOrEmpty(lastSentence)) 
                sentences.Add(lastSentence);
        }
        Debug.Log(string.Join(",", sentences));
        return sentences.ToArray();
    }
    #endregion
    
    private void giveWitnessTestimony(CaseGenerator.WitnessTestimony testimony)
    {
        string fullTestimony = GenerateFullTestimony(testimony);
        string[] sentences = SplitIntoSentences(fullTestimony);
        dialogueTextFlow.SetDialogueLines(sentences);
    }
}
