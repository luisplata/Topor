using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogicOfLevel : MonoBehaviour, ITimeLineService
{
    [SerializeField] private LevelStartController _levelStartController;
    [SerializeField] private FactoryOfTopos factoryOfTopos;
    [SerializeField] private Map map;
    private float deltaTimeLocal;
    private float totalTime;
    private bool isConfigured;
    private List<StepInGame> _steps;
    private int currentStepIndex;
    private StepInGame currentStepTime;
    private List<Topo> _topos;

    public bool GameIsEnded { get; private set; }

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ITimeLineService>(this);
    }

    private void Start()
    {
        _levelStartController = ServiceLocator.Instance.GetService<ISaveDataToLevels>().GetLevelStartController();
        _steps = new List<StepInGame>();
        _topos = new List<Topo>();
    }

    public void Configure(IGameLoop gameLoop)
    {
        //calculate how log the level will be
        totalTime = _levelStartController.TimeOfGame;
        var totalWeight = _levelStartController.Steps.Sum(step => step.weight);

        foreach (var step in _levelStartController.Steps)
        {
            var timeOfStep = (float)step.weight / totalWeight * totalTime;
            _steps.Add(new StepInGame
            {
                timeOfStep = timeOfStep,
                toposToSpawn = step.toposToSpawn
            });
        }
        
        currentStepTime = _steps[currentStepIndex];
        Debug.Log($"Total time: {totalTime}");
    }

    public void StartCount()
    {
        isConfigured = true;
    }

    public void StopGame()
    {
        isConfigured = false;
        foreach (var top in _topos)
        {
            Destroy(top.gameObject);
        }
    }

    private void Update()
    {
        if (!isConfigured || GameIsEnded) return;
        deltaTimeLocal += Time.deltaTime;
        foreach (var spawn in currentStepTime.toposToSpawn)
        {
            spawn.deltaSpawn += Time.deltaTime;
            if (spawn.deltaSpawn >= 60 / spawn.countOfEnemyToSpawnOfMin)
            {
                spawn.deltaSpawn = 0;
                Debug.Log($"Spawn {spawn.toposToSpawn}");
                var topo = factoryOfTopos.SpawnTopo(spawn.toposToSpawn, map.GetPointToTopoByPosition(map.GetRandomPositionToTopo()));
                topo.StartTopo();
                _topos.Add(topo);
            }
        }
        if (deltaTimeLocal >= currentStepTime.timeOfStep)
        {
            deltaTimeLocal = 0;
            currentStepIndex++;
            if (currentStepIndex >= _steps.Count)
            {
                GameIsEnded = true;
                return;
            }
            currentStepTime = _steps[currentStepIndex];
        }
    }
}