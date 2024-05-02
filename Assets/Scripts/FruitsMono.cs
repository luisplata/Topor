using System;
using System.Linq;
using UnityEngine;

public class FruitsMono : MonoBehaviour
{
    [SerializeField] private FactoryOfFruits factoryOfFruits;
    [SerializeField] private RelationFruit[] relationFruits;
    [SerializeField] private Map map;
    private TeaTime _spawn;
    private bool _allFruitsDead;
    public bool AllFruitAreDead => _allFruitsDead;

    public bool Finished { get; private set; }

    public void StartToSpawn()
    {
        Finished = false;
        _spawn.Play();
    }

    private void Start()
    {
        _spawn = this.tt().Pause().Loop(1,t =>
        {
            CalculateFruitsPercentage();
            foreach (var relationFruit in relationFruits)
            {
                Debug.Log(relationFruit.fruitId + " " + relationFruit.quantity);
                for (var i = 0; i < relationFruit.quantity; i++)
                {
                    var fruit = factoryOfFruits.SpawnFruit(relationFruit.fruitId, map.GetPointToFruitByPosition(map.GetRandomPositionToFruit()));
                    fruit.OnFruitDie += FruitOnOnFruitDie;
                }
            }
            t.Break();
        }).Add(() =>
        {
            Finished = true;
        });
    }

    private void FruitOnOnFruitDie()
    {
        //validate if all fruits are dead
        _allFruitsDead = map.GetAllFruits().All(pointToFruit => pointToFruit.GetFruit().AreDead);
    }

    private void CalculateFruitsPercentage()
    {
        var totalFruits = map.GetFruits();
        foreach (var relationFruit in relationFruits)
        {
            relationFruit.quantity = (int)(totalFruits * relationFruit.percentage) / 100;
            //Debug.Log(relationFruit.fruitId + " " + relationFruit.quantity + " to " + totalFruits);
        }
        var rest = totalFruits - relationFruits.Sum(relationFruit => relationFruit.quantity);
        //Debug.Log($"rest: {rest}");
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