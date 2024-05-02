public class SaveDataToLevels : ISaveDataToLevels
{
    private LevelStartController _levelStartController;
    public void SaveData(LevelStartController levelStartController)
    {
        _levelStartController = levelStartController;
    }

    public LevelStartController GetLevelStartController()
    {
        return _levelStartController;
    }
}