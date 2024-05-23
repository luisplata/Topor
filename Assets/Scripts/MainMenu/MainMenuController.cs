using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private NextScene nextScene;
    [SerializeField] private int nextSceneIndex;
    public void StartGame()
    {
        //oportinity to add some logic before start the game
        nextScene.NextTo(nextSceneIndex);
    }
}