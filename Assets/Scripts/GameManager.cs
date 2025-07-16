using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public StartTrialButton startTrialButton;
    private bool _trialCanStart;

    private CaseGenerator.CriminalRecord criminalRecord;
    private CaseGenerator.WitnessTestimony witnessTestimony;

    [SerializeField] private CaseGenerator caseGenerator;
    // caseGenerator.GenerateWitnessTestimony(getDossier.GetCriminalRecord());

    void Awake()
    {
        Instance = this;

        caseGenerator.InitializeGenerator();

        generateCriminalRecord();
        generateWitnessTestimony();

        // Debug.Log();

    }

    private void generateWitnessTestimony()
    {
        this.witnessTestimony = caseGenerator.GenerateWitnessTestimony(this.criminalRecord);
    }

    private void generateCriminalRecord()
    {
        this.criminalRecord = caseGenerator.GenerateCriminalRecord();
    }

    public void SetTrialCanStart(bool value)
    {
        _trialCanStart = value;
        if (_trialCanStart)
        {
            startTrialButton.Activate();
        }
    }

    public bool GetTrialCanStart()
    {
        return _trialCanStart;
    }

    public CaseGenerator.CriminalRecord GetCriminalRecord()
    {
        return criminalRecord;
    }
    
    public CaseGenerator.WitnessTestimony GetWitnessTestimony()
    {
        return witnessTestimony;
    }
    
}
