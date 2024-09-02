using UnityEngine;

public class SaveDataToLevels : ISaveDataToLevels
{
    private LevelStartController _levelStartController;
    public void SaveData(LevelStartController levelStartController)
    {
        _levelStartController = levelStartController;
    }

    public LevelStartController GetLevelStartController()
    {
        if (_levelStartController == null)
        {
            throw new System.Exception("LevelStartController is null");
        }
        return _levelStartController;
    }
}