using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class CaseGenerator : MonoBehaviour
{
    #region Crime Generation
    private string[] maleFirstNames = { "Иван", "Алексей", "Дмитрий", "Сергей", "Андрей" };
    private string[] maleLastNames = { "Смирнов", "Иванов", "Кузнецов", "Попов", "Соколов" };
    private string[] femaleFirstNames = { "Мария", "Елена", "Ольга", "Наталья", "Татьяна" };
    private string[] femaleLastNames = { "Смирнова", "Иванова", "Кузнецова", "Попова", "Соколова" };
    private string[] countries = { "Россия", "Беларусь", "Казахстан", "Украина", "Литва", "Латвия", "Эстония" };

    public enum CrimeType
    {
        Кража,
        Мошенничество,
        Хулиганство,
        Контрабанда,
        Саботаж
    }

    public class CriminalRecord
    {
        public string FirstName;
        public string LastName;
        public int Age;
        public string Residence;
        public string Gender;
        public CrimeType Crime;
        public List<string> Evidence;

        public override string ToString()
        {
            return $"Имя: {FirstName}\nФамилия: {LastName}\nВозраст: {Age}\nМесто проживания: {Residence}\nПол: {Gender}\nПреступление: {Crime}\nУлики: {string.Join(", ", Evidence)}";
        }
    }

    private Dictionary<CrimeType, List<string>> evidencePools = new Dictionary<CrimeType, List<string>>();


    public EvidenceBox EvidenceBox;

    public void InitializeGenerator()
    {
        InitializeEvidencePools();
        InitializeTestimonyTemplates();
    }

    private void InitializeEvidencePools()
    {
        evidencePools[CrimeType.Кража] = new List<string> { "Отмычка", "Украденный кошелек", "Следы обуви", "Сумка с добычей" };
        evidencePools[CrimeType.Мошенничество] = new List<string> { "Поддельный документ", "Фальшивые деньги", "Договор с подписью", "Список жертв" };
        evidencePools[CrimeType.Хулиганство] = new List<string> { "Бита", "Разбитое стекло", "Баллончик с краской", "Свидетельские показания" };
        evidencePools[CrimeType.Контрабанда] = new List<string> { "Запрещенный товар", "Поддельные документы", "Спрятанный груз", "Ключ от тайника" };
        evidencePools[CrimeType.Саботаж] = new List<string> { "Инструменты", "Схема объекта", "Поврежденное оборудование", "Угрожающее письмо" };
    }

    public CriminalRecord GenerateCriminalRecord()
    {
        CriminalRecord record = new CriminalRecord();

        bool isMale = Random.Range(0, 2) == 0;
        record.Gender = isMale ? "Мужской" : "Женский";

        if (isMale)
        {
            record.FirstName = maleFirstNames[Random.Range(0, maleFirstNames.Length)];
            record.LastName = maleLastNames[Random.Range(0, maleLastNames.Length)];
        }
        else
        {
            record.FirstName = femaleFirstNames[Random.Range(0, femaleFirstNames.Length)];
            record.LastName = femaleLastNames[Random.Range(0, femaleLastNames.Length)];
        }

        record.Age = Random.Range(18, 70);
        record.Residence = countries[Random.Range(0, countries.Length)];
        record.Crime = (CrimeType)Random.Range(0, Enum.GetValues(typeof(CrimeType)).Length);
        record.Evidence = GenerateEvidence(record.Crime);

        return record;
    }

    private List<string> GenerateEvidence(CrimeType crimeType)
    {
        List<string> evidenceList = new List<string>();
        List<string> pool = evidencePools[crimeType];
        int evidenceCount = Random.Range(1, 4);

        for (int i = 0; i < evidenceCount; i++)
        {
            string evidenceItem = pool[Random.Range(0, pool.Count)];
            evidenceList.Add(evidenceItem);

            // if (EvidenceBox != null)
            // {
            //     EvidenceBox.AddEvidence(evidenceItem);
            // }
        }
        evidenceList = new List<string>(new HashSet<string>(evidenceList));
        if (EvidenceBox != null)
        {
            foreach (var evidenceItem in evidenceList)
            {
                EvidenceBox.AddEvidence(evidenceItem);
            }
        }

        return evidenceList;
    }
    #endregion

    #region Witness Testimony Generation

    private Dictionary<CrimeType, List<string>> truthfulTemplates = new Dictionary<CrimeType, List<string>>();
    private List<string> neutralTemplates = new List<string>();

    public enum TestimonyType
    {
        Правдивое,
        Ложное,
        Нейтральное
    }

    public class WitnessTestimony
    {
        public TestimonyType Type;
        public string Description;
        public CrimeType? RelevantCrime;
    }

    private void InitializeTestimonyTemplates()
    {
        truthfulTemplates[CrimeType.Кража] = new List<string> {
            "Видел как подозреваемый использовал {0} возле места преступления.",
            "Заметил {0} в руках подозреваемого в тот день.",
            "Слышал звон {0} из сумки подозреваемого."
        };
        
        truthfulTemplates[CrimeType.Мошенничество] = new List<string> {
            "Подозреваемый показывал кому-то {0}.",
            "Видел как подозреваемый обменивал {0}.",
            "Слышал разговор о {0} из уст подозреваемого."
        };
        
        truthfulTemplates[CrimeType.Хулиганство] = new List<string> {
            "Подозреваемый размахивал {0} перед инцидентом.",
            "Видел {0} в действии подозреваемого.",
            "Слышал угрозы с упоминанием {0}."
        };
        
        truthfulTemplates[CrimeType.Контрабанда] = new List<string> {
            "Заметил {0} в скрытом месте у подозреваемого.",
            "Подозреваемый пытался спрятать {0} при приближении стражи.",
            "Видел передачу {0} в подозрительной обстановке."
        };
        
        truthfulTemplates[CrimeType.Саботаж] = new List<string> {
            "Подозреваемый возился с {0} на месте аварии.",
            "Видел {0} в руках подозреваемого перед поломкой.",
            "Слышал разговор о применении {0} для повреждения."
        };

        neutralTemplates = new List<string> {
            "Ничего необычного не заметил.",
            "Не могу вспомнить что-то конкретное.",
            "В тот день было много подозрительных лиц.",
            "Не уверен, что это было связано с преступлением.",
            "Не помню деталей происшествия."
        };
    }

    public List<WitnessTestimony> GenerateWitnessTestimonies(CriminalRecord record)
    {
        List<WitnessTestimony> testimonies = new List<WitnessTestimony>();
        int numberOfTestimonies = Random.Range(2, 5); // Генерируем случайное количество показаний от 2 до 4

        for (int i = 0; i < numberOfTestimonies; i++)
        {
            WitnessTestimony testimony = new WitnessTestimony();
            float randomValue = Random.value;
            const string evidenceColor = "#FF0000";

            if (randomValue < 0.6f) // 60% правдивые
            {
                testimony.Type = TestimonyType.Правдивое;
                testimony.RelevantCrime = record.Crime;
                string evidence = record.Evidence[Random.Range(0, record.Evidence.Count)];
                string template = truthfulTemplates[record.Crime][Random.Range(0, truthfulTemplates[record.Crime].Count)];
                testimony.Description = string.Format(template, $"<color={evidenceColor}>{evidence}</color>");
            }
            else if (randomValue < 0.9f) // 30% ложные
            {
                testimony.Type = TestimonyType.Ложное;

                // Выбираем случайное преступление, отличное от реального
                CrimeType falseCrime;
                do
                {
                    falseCrime = (CrimeType)Random.Range(0, Enum.GetValues(typeof(CrimeType)).Length);
                } while (falseCrime == record.Crime);

                testimony.RelevantCrime = falseCrime;
                string falseEvidence = evidencePools[falseCrime][Random.Range(0, evidencePools[falseCrime].Count)];
                string template = truthfulTemplates[falseCrime][Random.Range(0, truthfulTemplates[falseCrime].Count)];
                testimony.Description = string.Format(template, $"<color={evidenceColor}>{falseEvidence}</color>");
            }
            else // 10% нейтральные
            {
                testimony.Type = TestimonyType.Нейтральное;
                testimony.RelevantCrime = null;
                testimony.Description = neutralTemplates[Random.Range(0, neutralTemplates.Count)];
            }

            testimonies.Add(testimony); // Добавляем показание в список
    }

    return testimonies; // Возвращаем список показаний
    }   

    #endregion
}

