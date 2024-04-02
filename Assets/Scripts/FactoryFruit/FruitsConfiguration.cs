using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/FruitsConfiguration", fileName = "FruitsConfiguration", order = 0)]
public class FruitsConfiguration : ScriptableObject
{
    [SerializeField] private Fruit[] fruits;
    private Dictionary<string, Fruit> idToFruit;

    private void Awake()
    {
        idToFruit = new Dictionary<string, Fruit>(fruits.Length);
        foreach (var fruit in fruits)
        {
            idToFruit.Add(fruit.Id, fruit);
        }
    }

    public Fruit GetFruitPrefabById(string id)
    {
        if (!idToFruit.TryGetValue(id, out var fruit))
        {
            throw new Exception($"Fruit with id {id} does not exit");
        }
        return fruit;
    }
}