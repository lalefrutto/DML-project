using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public StartTrialButton startTrialButton;
    private bool _trialCanStart;
    private int _dayCounter;

    private CaseGenerator.CriminalRecord criminalRecord;
    private List<CaseGenerator.WitnessTestimony> witnessTestimonies;
    private int _witnessCount;

    public int getWitnessCount()
    {
        return _witnessCount;
    }

    [SerializeField] private CaseGenerator caseGenerator;
    [SerializeField] private EvidenceBox evidenceBox;
    // caseGenerator.GenerateWitnessTestimony(getDossier.GetCriminalRecord());


    void Awake()
    {
        Instance = this;

        caseGenerator.InitializeGenerator();
        caseGenerator.EvidenceBox = evidenceBox;

        generateCriminalRecord();
        generateWitnessTestimony();

        // Debug.Log();

    }

    private void generateWitnessTestimony()
    {
        this.witnessTestimonies = caseGenerator.GenerateWitnessTestimonies(this.criminalRecord);
        _witnessCount = (witnessTestimonies as ICollection)?.Count ?? 0;
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

    public CaseGenerator.WitnessTestimony GetWitnessTestimonyFrom(int numberOfWitness)
    {
        // return witnessTestimonies[numberOfWitness];

        if (numberOfWitness >= 0 && numberOfWitness < witnessTestimonies.Count)
        {
            return witnessTestimonies[numberOfWitness];
        }
        else
        {
            return null;
        }
    }

    private void endDay()
    {
        _dayCounter++;
        // implementation
    }

    public int getDayNumber()
    {
        return _dayCounter;
    }
}
