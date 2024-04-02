using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    [SerializeField] private string id;
    public string Id => id;

    public void Configure(GameObject parent)
    {
        
    }
}