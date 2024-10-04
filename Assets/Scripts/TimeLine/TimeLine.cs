using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Timeline", fileName = "TimeLine", order = 0)]
public class TimeLine : ScriptableObject
{
    [SerializeField] private string level;
    [SerializeField] private TimeLineStep[] steps;
    private List<TimeLineStep> _stepsInstance;
    
    public void Awake()
    {
        _stepsInstance = new List<TimeLineStep>();
        foreach (var step in steps)
        {
            _stepsInstance.Add(Instantiate(step));
        }
    }

    public List<TimeLineStep> GetSteps()
    {
        return _stepsInstance;
    }

    public float TotalTime()
    {
        //get the most high time from all steps
        return _stepsInstance.Max(step => step.GetTime());
    }
}