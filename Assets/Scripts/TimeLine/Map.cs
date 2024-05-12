using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour, IMap
{
    [SerializeField] private List<PointToTopo> pointToTopos;
    [SerializeField] private List<PointToFruit> pointToFruits;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IMap>(this);
        pointToTopos = new List<PointToTopo>();
        pointToFruits = new List<PointToFruit>();
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IMap>();
    }

    public PointToTopo GetPointToTopoByPosition(int position)
    {
        return (from pointToTopo in pointToTopos where pointToTopo.Position == position select pointToTopo).FirstOrDefault();
    }
    public PointToFruit GetPointToFruitByPosition(int position)
    {
        return (from pointToFruit in pointToFruits where pointToFruit.Position == position select pointToFruit).FirstOrDefault();
    }

    public int GetRandomPositionToTopo()
    {
        var listOfFreeTopos = pointToTopos.Where(point => point.IsFree).ToList();
        var position = Random.Range(0, listOfFreeTopos.Count);
        var result = listOfFreeTopos[position];
        return result.Position;
    }

    public void SaveTopo(PointToTopo topo)
    {
        pointToTopos.Add(topo);
    }

    public void SaveFruit(PointToFruit pointToFruit)
    {
        pointToFruits.Add(pointToFruit);
    }

    public int GetRandomPositionToFruit()
    {
        var position = Random.Range(0, pointToFruits.Count);
        var tries = 0;
        while (pointToFruits[position].HasFruit)
        {
            position = Random.Range(0, pointToFruits.Count);
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
        return pointToFruits.Count;
    }

    public List<PointToFruit> GetAllFruits()
    {
        return pointToFruits;
    }
}

public interface IMap
{
    int GetRandomPositionToTopo();
    void SaveTopo(PointToTopo topo);
    void SaveFruit(PointToFruit pointToFruit);
}