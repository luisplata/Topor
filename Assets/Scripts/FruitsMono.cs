using System;
using System.Linq;
using UnityEngine;

public class FruitsMono : MonoBehaviour
{
    [SerializeField] private FactoryOfFruits factoryOfFruits;
    [SerializeField] private RelationFruit[] relationFruits;
    [SerializeField] private Map map;
    private TeaTime _spawn;

    public bool Finished { get; private set; }

    public void StartToSpawn()
    {
        Finished = false;
        _spawn.Play();
    }

    private void Start()
    {
        CalculateFruitsPercentage();
        _spawn = this.tt().Pause().Loop(1,t =>
        {
            foreach (var relationFruit in relationFruits)
            {
                Debug.Log(relationFruit.fruitId + " " + relationFruit.quantity);
                for (var i = 0; i < relationFruit.quantity; i++)
                {
                    factoryOfFruits.SpawnFruit(relationFruit.fruitId, map.GetPointToFruitByPosition(map.GetRandomPositionToFruit()));
                }
            }
            t.Break();
        }).Add(() =>
        {
            Finished = true;
        });
    }

    private void CalculateFruitsPercentage()
    {
        var totalFruits = map.GetFruits();
        foreach (var relationFruit in relationFruits)
        {
            relationFruit.quantity = (int)(totalFruits * relationFruit.percentage) / 100;
            Debug.Log(relationFruit.fruitId + " " + relationFruit.quantity + " to " + totalFruits);
        }
        //fill the rest of the fruits
        var rest = totalFruits - relationFruits.Sum(relationFruit => relationFruit.quantity);
        //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"rest: {rest}");
        relationFruits[0].quantity += rest;
    }
}

[Serializable]
public class RelationFruit
{
    public string fruitId;
    public float percentage;
    public int quantity;
}