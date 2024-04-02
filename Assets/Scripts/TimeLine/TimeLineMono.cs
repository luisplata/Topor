using System;
using UnityEngine;

public class TimeLineMono : MonoBehaviour
{
    [SerializeField] private TimeLine timeLine;
    private float currentTime;
    private IGameLoop _gameLoop;
    private float _totalTime;
    private bool _weAreGaming;
    public bool GameIsEnded => !_weAreGaming;

    private void StartGame()
    {
        currentTime = 0;
        foreach (var step in timeLine.GetSteps())
        {
            step.IsDone = false;
        }
        
        _totalTime = timeLine.TotalTime() + 5;
        
        Debug.Log($"Total time: {_totalTime}");
    }
    
    private void Update()
    {
        if(!_weAreGaming) return;
        currentTime += Time.deltaTime;
        foreach (var step in timeLine.GetSteps())
        {
            if (step.GetTime() <= currentTime && !step.IsDone)
            {
                step.IsDone = true;
                Debug.Log($"step in {step.GetTime()} with topo {step.GetTopo()} and position {step.Position}");
            }
        }
        
        if (currentTime >= _totalTime)
        {
            _weAreGaming = false;
        }
    }

    public void Configure(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        StartGame();
    }

    public void StartCount()
    {
        _weAreGaming = true;
    }
}