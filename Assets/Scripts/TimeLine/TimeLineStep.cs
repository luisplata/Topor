using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Step", fileName = "TimeLineStep", order = 0)]
public class TimeLineStep : ScriptableObject
{
    [SerializeField] private float time;
    [SerializeField] private string topo;
    [SerializeField] private int position;
    public Action OnTopoDie;
    private Topo _topo;
    public bool IsDone { get; set; }
    public bool IsDead { get; private set; }

    public int Position => position == -1 ? ServiceLocator.Instance.GetService<IMap>().GetRandomPositionToTopo() : position;

    public float GetTime()
    {
        return time;
    }

    public string GetTopo()
    {
        return topo;
    }

    public void StartTopo()
    {
        _topo.gameObject.SetActive(true);
        _topo.StartTopo();
    }

    public void SaveTopo(Topo spawnTopo)
    {
        _topo = spawnTopo;
        IsDead = false;
        _topo.OnTopoDie += () =>
        {
            IsDead = true;
            OnTopoDie?.Invoke();
        };
        _topo.gameObject.SetActive(false);
    }

    public float GetTimeFromTopo()
    {
        return _topo.TotalTime;
    }
}