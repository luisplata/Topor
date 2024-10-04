using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

[Serializable]
public class StepOfGame
{
    public StepIdentifier stepIdentifier;
    private StepIdentifier StepIdentifierInstance;
    public int weight;
    public List<StepSpawnRatioEnemy> toposToSpawn;
    public TimeLine timeLineStep;
    private TimeLine _timeLineStepInstance;
    private float _timeOfStep;

    public float timeOfStep
    {
        get => _timeOfStep;
        set
        {
            _timeOfStep = value;
            if(StepIdentifierInstance) StepIdentifierInstance.IsDone = false;
        }
    }
    
    public StepOfGame()
    {
        if(!stepIdentifier) return;
        StepIdentifierInstance = Object.Instantiate(stepIdentifier);
    }

    public void SetTimeLine(TimeLine instantiate)
    {
        _timeLineStepInstance = instantiate;
    }

    public bool HasTimeLine()
    {
        return _timeLineStepInstance != null;
    }

    public TimeLine GetTimeLine()
    {
        return _timeLineStepInstance;
    }

    public void Configure()
    {
        if(timeLineStep == null) return;
        SetTimeLine(Object.Instantiate(timeLineStep));
    }
}

[Serializable]
public class StepSpawnRatioEnemy
{
    public string toposToSpawn;
    public float countOfEnemyToSpawnOfMin;
    public float deltaSpawn;
}


[Serializable]
public class StepInGame
{
    public StepIdentifier stepIdentifier;
    public float timeOfStep { get; set; }
    public List<StepSpawnRatioEnemy> toposToSpawn { get; set; }
}
