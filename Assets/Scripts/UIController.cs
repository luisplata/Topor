using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IUiControllerService
{
    [SerializeField] private GameObject startPanel, endGamePanel, animationPanel;
    [SerializeField] private Button startButton, endButton;
    [SerializeField] private TextMeshProUGUI titleEndGame, subtitleEndGame;
    private IGameLoop _gameLoop;
    public bool SelectedEndGame { get; private set; }
    public bool SelectedStartGame { get; private set; }

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IUiControllerService>(this);   
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IUiControllerService>();
    }

    public void Configure(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        SelectedEndGame = false;
        SelectedStartGame = false;
        
        startButton.onClick.AddListener(() =>
        {
            SelectedStartGame = true;
        });
        
        endButton.onClick.AddListener(() =>
        {
            SelectedEndGame = true;
        });
        startPanel.SetActive(false);
        endGamePanel.SetActive(false);
        animationPanel.SetActive(true);
    }

    public void SetTitleEndGame(string title)
    {
        titleEndGame.text = title;
    }

    public void SetSubtitleEndGame(string subtitle)
    {
        subtitleEndGame.text = subtitle;
    }

    public void ShowStartPanel()
    {
        animationPanel.SetActive(false);
        endGamePanel.SetActive(false);
        startPanel.SetActive(true);
    }
    
    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void ShowEndGamePanel(bool winOrLose)
    {
        endGamePanel.SetActive(true);
        startPanel.SetActive(false);
        animationPanel.SetActive(false);
    }

    public void HideEndGamePanel()
    {
        endGamePanel.SetActive(false);
    }

    public void ShowEndGameAnimation()
    {
        animationPanel.SetActive(true);
        startPanel.SetActive(false);
        endGamePanel.SetActive(false);
    }
}

public interface IUiControllerService
{
    void ShowStartPanel();
    bool SelectedStartGame { get; }
    bool SelectedEndGame { get; }
    void HideStartPanel();
    void ShowEndGamePanel(bool b);
    void HideEndGamePanel();
    void ShowEndGameAnimation();
    void Configure(IGameLoop gameLoop);
    void SetTitleEndGame(string title);
    void SetSubtitleEndGame(string subtitle);
}