using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField] private UIController uiController;
    [SerializeField] private TimeLineMono timeLineMono;
    [SerializeField] private FruitsMono fruitsMono;
    [SerializeField] private float timeAfterEnd;
    private TeaTime _idle, _ready, _game, _condition, _end;

    void Start()
    {
        ConfigureButtons();
        ConfigureGameLoop();
        _idle.Play();
    }

    private void ConfigureGameLoop()
    {
        _idle = this.tt().Pause().Add(() =>
        {
            //show animation or whatever
            uiController.ShowStartPanel();
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Idle");
        }).Add(() =>
        {
            _ready.Play();
        });
        
        _ready = this.tt().Pause().Add(() =>
        {
            //configure all components
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Ready");
        }).Wait(()=>uiController.SelectedStartGame).Add(() =>
        {
            _game.Play();
        });
        
        _game = this.tt().Pause().Add(() =>
        {
            uiController.HideStartPanel();
            fruitsMono.StartToSpawn();
        }).Wait(()=>fruitsMono.Finished).Add(() =>
        {
            timeLineMono.Configure(this);
        }).Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Game");
            timeLineMono.StartCount();
        }).Add(() =>
        {
            _condition.Play();
        });
        
        _condition = this.tt().Pause().Add(() =>
        {
            //Check condition
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Condition");
        }).Wait(()=>timeLineMono.GameIsEnded || fruitsMono.AllFruitAreDead).Add(() =>
        {
            _end.Play();
        });
        
        _end = this.tt().Pause().Add(() =>
        {
            if (timeLineMono.GameIsEnded)
            {
                ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Win");
            }else if (fruitsMono.AllFruitAreDead)
            {
                ServiceLocator.Instance.GetService<IDebugCustom>().DebugText("GameLoop: Lose");
            }
            uiController.ShowEndGamePanel(true);
        }).Wait(()=>uiController.SelectedEndGame).Add(() =>
        {
            uiController.HideEndGamePanel();
            uiController.ShowEndGameAnimation();
        }).Add(timeAfterEnd).Add(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void ConfigureButtons()
    {
        uiController.Configure(this);
    }
}

public interface IGameLoop
{
}