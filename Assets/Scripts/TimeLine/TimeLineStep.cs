using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Step", fileName = "TimeLineStep", order = 0)]
public class TimeLineStep : ScriptableObject
{
    [SerializeField] private float time;
    [SerializeField] private string topo;
    [SerializeField] private int position;
    public bool IsDone { get; set; }

    public int Position
    {
        get => position == -1 ? Random.Range(0, 10) : position;
        set => position = value;
    }

    public float GetTime()
    {
        return time;
    }

    public string GetTopo()
    {
        return topo;
    }
}