public interface ITimeLineService
{
    void Configure(IGameLoop gameLoop);
    void StartCount();
    bool GameIsEnded { get; }
    void StopGame();
    void IsPaused(bool isPaused);
    LevelStartController GetConfigOfLevel();
}