using UnityEngine;

public class TimeLineMono : MonoBehaviour, ITimeLineService
{
    [SerializeField] private TimeLine timeLine;
    [SerializeField] private FactoryOfTopos factoryOfTopos;
    [SerializeField] private Map map;
    private float currentTime;
    private IGameLoop _gameLoop;
    private float _totalTime;
    private bool _weAreGaming;
    public bool GameIsEnded => !_weAreGaming;

    private void Start()
    {
        ServiceLocator.Instance.RegisterService<ITimeLineService>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ITimeLineService>();
    }

    private void StartGame()
    {
        currentTime = 0;
        //Debug.Log($"Start game timeLine.GetSteps() {timeLine.GetSteps().Length}");
        foreach (var step in timeLine.GetSteps())
        {
            step.IsDone = false;
            var topo = factoryOfTopos.SpawnTopo(step.GetTopo(), map.GetPointToTopoByPosition(step.Position));
            step.SaveTopo(topo);
            step.OnTopoDie += OnTopoDie;
        }

        _totalTime = 0;
        foreach (var step in timeLine.GetSteps())
        {
            _totalTime += step.GetTimeFromTopo();
        }

        //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Total time: {_totalTime}");
    }

    private void OnTopoDie()
    {
        foreach (var step in timeLine.GetSteps())
        {
            if (!step.IsDead) return;
        }
        _weAreGaming = false;
        //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("All topos are dead");
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
                step.StartTopo();
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

    public void StopGame()
    {
        _weAreGaming = false;
    }
}

public interface ITimeLineService
{
    void Configure(IGameLoop gameLoop);
    void StartCount();
    bool GameIsEnded { get; }
    void StopGame();
}