using System;
using UnityEngine;

public class PointToTopo : MonoBehaviour
{
    [SerializeField] private int position;
    [SerializeField] private bool isFree = true;
    [SerializeField] private bool otherTopoRised;
    [SerializeField] private Animator animator;

    private void Reset()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public int Position => position;
    public bool IsFree => isFree;
    
    public void SetFree(bool free)
    {
        isFree = free;
    }
    
    public void OutFromGround()
    {
        animator.SetTrigger("outOfGround");
    }

    private void Start()
    {
        ServiceLocator.Instance.GetService<IMap>().SaveTopo(this);
    }

    public bool OtherTopoOutOfGround()
    {
        return otherTopoRised;
    }
}