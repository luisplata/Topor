using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bellseboss/SelectorLevel/StepIdentifier", fileName = "StepIdentifier", order = 0)]
public class StepIdentifier : ScriptableObject
{
    public string Id;
    public bool IsDone = true;
    
    public StepIdentifier()
    {
        Id = Guid.NewGuid().ToString();
    }
}