using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogicOfLevel : MonoBehaviour, ITimeLineService
{
    [SerializeField] private LevelStartController _levelStartController;
    [SerializeField] private FactoryOfTopos factoryOfTopos;
    [SerializeField] private Map map;
    [SerializeField] private TimeLightsSystem timeLightsSystem;
    private LevelStartController _levelStartControllerInstance;
    [SerializeField] private float deltaTimeLocal, _deltaTimeGlobal, _deltaTimeToStartStep;
    private float totalTime;
    private bool isConfigured, _isPaused;
    private List<StepOfGame> _steps;
    private int currentStepIndex;
    private StepOfGame currentStepTime;
    private List<Topo> _topos;

    public bool GameIsEnded { get; private set; }

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ITimeLineService>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ITimeLineService>();
    }

    private void Start()
    {
        try
        {
            _levelStartController = ServiceLocator.Instance.GetService<ISaveDataToLevels>().GetLevelStartController();
        }
        catch (Exception e)
        {
            Debug.Log($"Level Load from local storage failed: {e.Message}");
        }

        _levelStartControllerInstance = Instantiate(_levelStartController);

        _steps = new List<StepOfGame>();
        _topos = new List<Topo>();
        timeLightsSystem.Configure(this);
    }

    public void Configure(IGameLoop gameLoop)
    {
        //calculate how log the level will be
        totalTime = _levelStartControllerInstance.TimeOfGame;
        var totalWeight = _levelStartControllerInstance.Steps.Sum(step => step.weight);

        foreach (var step in _levelStartControllerInstance.Steps)
        {
            var timeOfStep = (float)step.weight / totalWeight * totalTime;
            step.timeOfStep = timeOfStep;
            step.Configure();
            _steps.Add(step);
        }

        currentStepTime = _steps[currentStepIndex];
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

    public void IsPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void Update()
    {
        if (!isConfigured || GameIsEnded || _isPaused) return;
        deltaTimeLocal += Time.deltaTime;
        _deltaTimeGlobal += Time.deltaTime;
        _deltaTimeToStartStep += Time.deltaTime;

        if (!currentStepTime.HasTimeLine())
        {
            foreach (var spawn in currentStepTime.toposToSpawn)
            {
                spawn.deltaSpawn += Time.deltaTime;
                if (spawn.deltaSpawn >= (60 / spawn.countOfEnemyToSpawnOfMin))
                {
                    spawn.deltaSpawn = 0;
                    var topo = factoryOfTopos.SpawnTopo(spawn.toposToSpawn,
                        map.GetPointToTopoByPosition(map.GetRandomPositionToTopo()));
                    topo.StartTopo();
                    _topos.Add(topo);
                }
            }
        }
        else
        {
            foreach (var timeLineStep in currentStepTime.GetTimeLine().GetSteps())
            {
                if (_deltaTimeToStartStep >= timeLineStep.GetTime() && !timeLineStep.IsDone)
                {
                    var topo = factoryOfTopos.SpawnTopo(timeLineStep.GetTopo(),
                        map.GetPointToTopoByPosition(timeLineStep.Position));
                    topo.StartTopo();
                    _topos.Add(topo);
                    timeLineStep.IsDone = true;
                    break;
                }
            }
            //check if all steps are done
            var isAllStepsDone = true;
            foreach (var timeLineStep in currentStepTime.GetTimeLine().GetSteps())
            {
                if (!timeLineStep.IsDone)
                {
                    isAllStepsDone = false;
                    break;
                }
            }
            
            if (isAllStepsDone)
            {
                currentStepTime.stepIdentifier.IsDone = true;
            }
        }

        if (deltaTimeLocal >= currentStepTime.timeOfStep && currentStepTime.stepIdentifier.IsDone)
        {
            deltaTimeLocal = 0;
            currentStepIndex++;
            if (currentStepIndex >= _steps.Count)
            {
                GameIsEnded = true;
                return;
            }

            currentStepTime = _steps[currentStepIndex];
            _deltaTimeToStartStep = 0;
        }

        timeLightsSystem.SetInterval(_deltaTimeGlobal / totalTime);
    }
}