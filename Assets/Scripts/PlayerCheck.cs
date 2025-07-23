using System.Collections.Generic;
using UnityEngine;
using TestimonyType = CaseGenerator.TestimonyType;

public class PlayerCheck : MonoBehaviour
{
    // private List<TestimonyType> testimonyTypes = new List<TestimonyType>();
    // private List<TestimonyType> testimonyTypesS= new List<TestimonyType>();
    private bool _isGuiltyCase;

    void Start()
    {
        _isGuiltyCase = GameManager.Instance.GetCriminalRecord().IsGuilty;
    }
    
    // public void AddTestimonyType(TestimonyType type)
    // {
    //     testimonyTypes.Add(type);
    // }
}
