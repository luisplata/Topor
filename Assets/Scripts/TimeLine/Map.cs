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

    public int GetRandomPositionToTopo()
    {
        return UnityEngine.Random.Range(0, pointToTopos.Length);
    }
}

public interface IMap
{
    int GetRandomPositionToTopo();
}