using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField] private UIController uiController;
    [SerializeField] private TimeLineMono timeLineMono;
    private TeaTime _idle, _ready, _game, _condition, _end;

    void Start()
    {
        ConfigureButtons();
        ConfigureGameLoop();
        timeLineMono.Configure(this);
        _idle.Play();
    }

    private void ConfigureGameLoop()
    {
        _idle = this.tt().Pause().Add(() =>
        {
            //show animation or whatever
            uiController.ShowStartPanel();
            Debug.Log("GameLoop: Idle");
        }).Add(() =>
        {
            _ready.Play();
        });
        
        _ready = this.tt().Pause().Add(() =>
        {
            //configure all components
            Debug.Log("GameLoop: Ready");
            Debug.Log($"GameLoop: uiController.SelectedStartGame {uiController.SelectedStartGame}");
        }).Wait(()=>uiController.SelectedStartGame).Add(() =>
        {
            _game.Play();
        });
        
        _game = this.tt().Pause().Add(() =>
        {
            uiController.HideStartPanel();
            Debug.Log("GameLoop: Game");
            timeLineMono.StartCount();
        }).Add(() =>
        {
            _condition.Play();
        });
        
        _condition = this.tt().Pause().Add(() =>
        {
            //Check condition
            Debug.Log("GameLoop: Condition");
        }).Wait(()=>timeLineMono.GameIsEnded).Add(() =>
        {
            _end.Play();
        });
        
        _end = this.tt().Pause().Add(() =>
        {
            Debug.Log("GameLoop: End");
            uiController.ShowEndGamePanel(true);
            Debug.Log($"GameLoop: uiController.SelectedEndGame {uiController.SelectedEndGame}");
        }).Wait(()=>uiController.SelectedEndGame).Add(() =>
        {
            uiController.HideEndGamePanel();
            //show animation or whatever
            uiController.ShowEndGameAnimation();
            Debug.Log("GameLoop: End Clicked");
        }).Add(10).Add(() =>
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