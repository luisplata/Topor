using UnityEngine;

public class FruitsMono : MonoBehaviour
{
    [SerializeField] private FactoryOfFruits factoryOfFruits;
    [SerializeField] private Map map;
    private TeaTime _spawn;

    public bool Finished { get; private set; }

    public void StartToSpawn()
    {
        Finished = false;
        _spawn.Play();
    }

    private void Start()
    {
        _spawn = this.tt().Pause().Add(5).Add(() =>
        {
            Finished = true;
        });
    }
}