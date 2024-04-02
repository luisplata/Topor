using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/ToposConfiguration", fileName = "ToposConfiguration", order = 0)]
public class ToposConfiguration : ScriptableObject
{
    [SerializeField] private Topo[] topos;
    private Dictionary<string, Topo> idToTopo;

    private void Awake()
    {
        idToTopo = new Dictionary<string, Topo>(topos.Length);
        foreach (var topo in topos)
        {
            idToTopo.Add(topo.Id, topo);
        }
    }

    public Topo GetTopoPrefabById(string id)
    {
        if (!idToTopo.TryGetValue(id, out var topo))
        {
            throw new Exception($"Topo with id {id} does not exit");
        }
        return topo;
    }
}