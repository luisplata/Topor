using System;
using UnityEngine;

public class PointToTopo : MonoBehaviour
{
    [SerializeField] private int position;
    public int Position => position;

    private void Start()
    {
        ServiceLocator.Instance.GetService<IMap>().SaveTopo(this);
    }

    public bool CanOutOfGround()
    {
        return false;
    }
}