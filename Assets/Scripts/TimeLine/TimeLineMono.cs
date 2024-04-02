using System;
using UnityEngine;

public class TimeLineMono : MonoBehaviour
{
    [SerializeField] private TimeLine timeLine;
    private float currentTime;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentTime = 0;
        foreach (var step in timeLine.GetSteps())
        {
            step.IsDone = false;
        }
    }
    
    private void Update()
    {
        currentTime += Time.deltaTime;
        foreach (var step in timeLine.GetSteps())
        {
            if (step.GetTime() <= currentTime && !step.IsDone)
            {
                step.IsDone = true;
                Debug.Log($"step in {step.GetTime()} with topo {step.GetTopo()} and position {step.Position}");
            }
        }
    }
    
}