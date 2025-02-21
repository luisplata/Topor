using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField] private FruitsMono fruitsMono;
    [SerializeField] private float timeAfterEnd;
    [SerializeField] private int nextScene;
    [SerializeField] private TouchTopo touchTopo;
    private TeaTime _idle, _ready, _game, _condition, _end;
    private TeaTime _currentTeaTime;

    void Start()
    {
        ConfigureButtons();
        ConfigureGameLoop();
        var confi = ServiceLocator.Instance.GetService<ITimeLineService>().GetConfigOfLevel();
        _currentTeaTime = confi.CanPlayCinematic ? _idle : _ready;
        _currentTeaTime.Play();
        //suscribe to the event of the pause button
        ServiceLocator.Instance.GetService<IFloatingPause>().OnPause += isPause =>
        {
            if (isPause)
            {
                touchTopo.CanPlaySounds(!isPause);
                _currentTeaTime.Pause();
            }
            else
            {
                _currentTeaTime.Play();
                touchTopo.CanPlaySounds(!isPause);
            }

            ServiceLocator.Instance.GetService<ITimeLineService>().IsPaused(isPause);
        };
    }

    private void ConfigureGameLoop()
    {
        _idle = this.tt().Pause().Add(() =>
        {
            touchTopo.CanPlaySounds(false);
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowAnimationStart();
        }).Wait(() => ServiceLocator.Instance.GetService<IUiControllerService>().AnimationStartGame).Add(() => { }).Add(
            () =>
            {
                _currentTeaTime = _ready;
                _currentTeaTime.Play();
            });

        _ready = this.tt().Pause().Add(() =>
        {
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowStartPanel();
            touchTopo.CanPlaySounds(false);
        }).Wait(() => ServiceLocator.Instance.GetService<IUiControllerService>().SelectedStartGame).Add(() =>
        {
            _currentTeaTime = _game;
            _currentTeaTime.Play();
        });

        _game = this.tt().Pause().Add(() =>
            {
                touchTopo.CanPlaySounds(true);
                ServiceLocator.Instance.GetService<IUiControllerService>().HideStartPanel();
                ServiceLocator.Instance.GetService<IUiControllerService>().ShowUiOfGame();
                fruitsMono.StartToSpawn();
            }).Wait(() => fruitsMono.Finished)
            .Add(() => { ServiceLocator.Instance.GetService<ITimeLineService>().Configure(this); }).Add(() =>
            {
                ServiceLocator.Instance.GetService<ITimeLineService>().StartCount();
            }).Add(() =>
            {
                _currentTeaTime = _condition;
                _currentTeaTime.Play();
            });

        _condition = this.tt().Pause().Add(() => { }).Wait(() =>
            ServiceLocator.Instance.GetService<ITimeLineService>().GameIsEnded || fruitsMono.AllFruitAreDead).Add(() =>
        {
            _currentTeaTime = _end;
            _currentTeaTime.Play();
            touchTopo.CanPlaySounds(false);
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
        }).Wait(() => ServiceLocator.Instance.GetService<IUiControllerService>().SelectedEndGame).Add(() =>
        {
            ServiceLocator.Instance.GetService<IUiControllerService>().HideEndGamePanel();
            ServiceLocator.Instance.GetService<IUiControllerService>().ShowEndGameAnimation(fruitsMono.AllFruitAreDead);
        }).Wait(() => ServiceLocator.Instance.GetService<IUiControllerService>().AnimationStartGame).Add(() =>
        {
            SceneManager.LoadScene(nextScene + 1);
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