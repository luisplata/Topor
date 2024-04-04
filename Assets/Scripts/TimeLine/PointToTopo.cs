using UnityEngine;

public class PointToTopo : MonoBehaviour
{
    [SerializeField] private int position;
    public int Position => position;

    public bool CanOutOfGround()
    {
        return false;
    }
}