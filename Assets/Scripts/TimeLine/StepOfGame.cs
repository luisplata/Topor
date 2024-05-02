using System;
using System.Collections.Generic;

[Serializable]
public class StepOfGame
{
    public int weight;
    public List<StepSpawnRatioEnemy> toposToSpawn;
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
    public float timeOfStep { get; set; }
    public List<StepSpawnRatioEnemy> toposToSpawn { get; set; }
}