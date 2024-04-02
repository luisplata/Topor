using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Timeline", fileName = "TimeLine", order = 0)]
public class TimeLine : ScriptableObject
{
    [SerializeField] private string level;
    [SerializeField] private TimeLineStep[] steps;
    
    public TimeLineStep[] GetSteps()
    {
        return steps;
    }

    public float TotalTime()
    {
        //get the most high time from all steps
        return steps.Max(step => step.GetTime());
    }
}