using UnityEngine;

internal class FruitsFactory
{
    private readonly FruitsConfiguration _config;

    public FruitsFactory(FruitsConfiguration config)
    {
        _config = config;
    }

    public Fruit Create(string id)
    {
        var prefab = _config.GetFruitPrefabById(id);

        return Object.Instantiate(prefab);
    }
}