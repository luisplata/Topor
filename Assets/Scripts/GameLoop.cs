using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField] private FruitsMono fruitsMono;
    [SerializeField] private float timeAfterEnd;
    [SerializeField] private int nextScene;
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
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowAnimationStart();
        }).Wait(()=>ServiceLocator.Instance.GetService<IUiControllerService>().AnimationStartGame).Add(() =>
        {
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowStartPanel();
        }).Add(() =>
        {
            _ready.Play();
        });
        
        _ready = this.tt().Pause().Add(() =>
        {
        }).Wait(()=>ServiceLocator.Instance.GetService<IUiControllerService>().SelectedStartGame).Add(() =>
        {
            _game.Play();
        });
        
        _game = this.tt().Pause().Add(() =>
        {
            ServiceLocator.Instance.GetService<IUiControllerService>().HideStartPanel();
            fruitsMono.StartToSpawn();
        }).Wait(()=>fruitsMono.Finished).Add(() =>
        {
            ServiceLocator.Instance.GetService<ITimeLineService>().Configure(this);
        }).Add(() =>
        {
            ServiceLocator.Instance.GetService<ITimeLineService>().StartCount();
        }).Add(() =>
        {
            _condition.Play();
        });
        
        _condition = this.tt().Pause().Add(() =>
        {
        }).Wait(()=>ServiceLocator.Instance.GetService<ITimeLineService>().GameIsEnded || fruitsMono.AllFruitAreDead).Add(() =>
        {
            _end.Play();
        });
        
        _end = this.tt().Pause().Add(() =>
        {
            if (fruitsMono.AllFruitAreDead)
            {
                ServiceLocator.Instance.GetService<IUiControllerService>().SetTitleEndGame("You Lose!");
                ServiceLocator.Instance.GetService<IUiControllerService>().SetSubtitleEndGame("All Fruits are Dead!");
            }
            else
            {
                ServiceLocator.Instance.GetService<IUiControllerService>().SetTitleEndGame("You Win!");
                ServiceLocator.Instance.GetService<IUiControllerService>().SetSubtitleEndGame("You save a few fruits!");
            }
            ServiceLocator.Instance.GetService<ITimeLineService>().StopGame();
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowEndGamePanel(true);
        }).Wait(()=>ServiceLocator.Instance.GetService<IUiControllerService>().SelectedEndGame).Add(() =>
        {
            ServiceLocator.Instance.GetService<IUiControllerService>().HideEndGamePanel();
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowEndGameAnimation(fruitsMono.AllFruitAreDead);
        }).Wait(()=>ServiceLocator.Instance.GetService<IUiControllerService>().AnimationStartGame).Add(() =>
        {
            SceneManager.LoadScene(nextScene);
        });
    }

    private void ConfigureButtons()
    {
        ServiceLocator.Instance.GetService<IUiControllerService>().Configure(this);
    }
}

public interface IGameLoop
{
}