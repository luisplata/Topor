using System;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour, IMap
{
    [SerializeField] private PointToTopo[] pointToTopos;
    [SerializeField] private PointToFruit[] pointToFruits;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IMap>(this);        
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IMap>();
    }

    public GameObject GetPointToTopoByPosition(int position)
    {
        return (from pointToTopo in pointToTopos where pointToTopo.Position == position select pointToTopo.gameObject).FirstOrDefault();
    }
    public GameObject GetPointToFruitByPosition(int position)
    {
        return (from pointToFruit in pointToFruits where pointToFruit.Position == position select pointToFruit.gameObject).FirstOrDefault();
    }

    public int GetRandomPositionToTopo()
    {
        return UnityEngine.Random.Range(0, pointToTopos.Length);
    }
    public int GetRandomPositionToFruit()
    {
        var position = UnityEngine.Random.Range(0, pointToFruits.Length);
        var tries = 0;
        while (pointToFruits[position].HasFruit)
        {
            position = UnityEngine.Random.Range(0, pointToFruits.Length);
            tries++;
            if (tries > 100)
            {
                Debug.LogError("No more space for fruits");
                break;
            }
        }
        pointToFruits[position].HasFruit = true;
        return position;
    }

    public int GetFruits()
    {
        return pointToFruits.Length;
    }
}

public interface IMap
{
    int GetRandomPositionToTopo();
}