using System;
using UnityEngine;

public class FactoryOfTopos : MonoBehaviour
{
    [SerializeField] private ToposConfiguration toposConfiguration;
    private ToposFactory _toposFactory;

    private void Awake()
    {
        _toposFactory = new ToposFactory(Instantiate(toposConfiguration));
    }
    
    public Topo SpawnTopo(string id, PointToTopo parent)
    {
        var topo = _toposFactory.Create(id);
        topo.Configure(parent);
        return topo;
    }

}