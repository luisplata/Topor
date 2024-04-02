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
    
}