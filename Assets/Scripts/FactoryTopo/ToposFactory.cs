    using UnityEngine;

public class ToposFactory
{
    private readonly ToposConfiguration powerUpsConfiguration;

    public ToposFactory(ToposConfiguration powerUpsConfiguration)
    {
        this.powerUpsConfiguration = powerUpsConfiguration;
    }
        
    public Topo Create(string id)
    {
        var prefab = powerUpsConfiguration.GetTopoPrefabById(id);

        return Object.Instantiate(prefab);
    }
}