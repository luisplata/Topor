using UnityEngine;

public class FactoryOfFruits : MonoBehaviour
{
    [SerializeField] private FruitsConfiguration toposConfiguration;
    private FruitsFactory _toposFactory;

    private void Awake()
    {
        _toposFactory = new FruitsFactory(Instantiate(toposConfiguration));
    }
    
    public Fruit SpawnFruit(string id, PointToFruit parent)
    {
        var fruit = _toposFactory.Create(id);
        fruit.Configure(parent);
        parent.SetFruit(fruit);
        return fruit;
    }
}