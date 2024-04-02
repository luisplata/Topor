using UnityEngine;

public class PointToTopo : MonoBehaviour
{
    [SerializeField] private int position;
    [SerializeField] private PointToFruit fruitLeft, fruitRight, fruitTop, fruitBottom;
    public int Position => position;
    
    public void GetFisrtFruit()
    {
    }
}