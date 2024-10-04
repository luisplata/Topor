using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Bellseboss/SelectorLevel/LevelStart", fileName = "LevelStartController", order = 0)]
public class LevelStartController : ScriptableObject
{
    [SerializeField] private int timeOfGame;
    [SerializeField] private List<StepOfGame> steps;
    [SerializeField] private int levelIndex;

    public void Awake()
    {
        foreach (var stepSpawnRatioEnemy in steps.SelectMany(step => step.toposToSpawn))
        {
            stepSpawnRatioEnemy.deltaSpawn = 0;
        }

        foreach (var step in steps.Where(step => step.timeLineStep != null))
        {
            step.Configure();
        }
    }

    public int TimeOfGame => timeOfGame;
    public List<StepOfGame> Steps => steps;
    public int LevelIndex => levelIndex + 3;
}