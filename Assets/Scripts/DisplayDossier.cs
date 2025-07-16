using UnityEngine;
using TMPro;

public class DisplayDossier : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI dossierText; // Текстовый элемент для отображения
    [SerializeField] private CaseGenerator caseGenerator; // Ссылка на генератор

    private CaseGenerator.CriminalRecord criminalRecord;
    
    public CaseGenerator.CriminalRecord GetCriminalRecord()
    {
        return criminalRecord;
    }

    //start
    void Awake()
    {
        // criminalRecord = caseGenerator.GenerateCriminalRecord();

        criminalRecord = GameManager.Instance.GetCriminalRecord();
        UpdateDossierText();
    }

    private void UpdateDossierText()
    {

        CaseGenerator.CriminalRecord record = caseGenerator.GenerateCriminalRecord();
        
        string formattedText = $"<b>ДОСЬЕ ПРЕСТУПНИКА</b>\n\n";
        formattedText += $"<b>Имя:</b> {record.FirstName}\n";
        formattedText += $"<b>Фамилия:</b> {record.LastName}\n";
        formattedText += $"<b>Возраст:</b> {record.Age}\n";
        formattedText += $"<b>Место проживания:</b> {record.Residence}\n";
        formattedText += $"<b>Пол:</b> {record.Gender}\n";
        formattedText += $"<b>Преступление:</b> {record.Crime.ToString()}\n\n";
        formattedText += "<b>УЛИКИ:</b>\n";
        
        foreach (string evidence in record.Evidence)
        {
            formattedText += $"- {evidence}\n";
        }

        dossierText.text = formattedText;
    }
}