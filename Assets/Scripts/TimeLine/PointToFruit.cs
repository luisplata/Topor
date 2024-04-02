using Unity.Collections;
using UnityEngine;

public class PointToFruit : MonoBehaviour
{
    [SerializeField] private int position;
    [ReadOnly][SerializeField] private bool hasFruit;
    public int Position => position;
    public bool HasFruit
    {
        get => hasFruit;
        set => hasFruit = value;
    }
}