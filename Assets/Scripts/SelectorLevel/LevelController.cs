using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelStartController levelStartController;
    [SerializeField] private NextScene nextScene;
    
    public void StartLevel()
    {
        //oportinity to add some logic before start the level
        ServiceLocator.Instance.GetService<ISaveDataToLevels>().SaveData(levelStartController);
        nextScene.NextTo(levelStartController.LevelIndex);
    }
}