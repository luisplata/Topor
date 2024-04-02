using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel, endGamePanel, animationPanel;
    [SerializeField] private Button startButton, endButton;
    private IGameLoop _gameLoop;
    public bool SelectedEndGame { get; private set; }
    public bool SelectedStartGame { get; private set; }

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