using Unity.Collections;
using UnityEngine;

public class PointToFruit : MonoBehaviour
{
    [SerializeField] private int position;
    [ReadOnly][SerializeField] private bool hasFruit;
    private Fruit fruit;
    public int Position => position;
    public bool HasFruit
    {
        get => hasFruit;
        set => hasFruit = value;
    }

    public void SetFruit(Fruit fruit)
    {
        this.fruit = fruit;
    }
    
    public Fruit GetFruit()
    {
        return fruit;
    }
}