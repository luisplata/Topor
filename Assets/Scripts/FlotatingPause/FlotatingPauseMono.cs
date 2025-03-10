using System;
using UnityEngine;

public class FlotatingPauseMono : MonoBehaviour, IFloatingPause
{
    [SerializeField] private Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");
    public event Action<bool> OnPause;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IFloatingPause>(this);
    }

    private void Start()
    {
        if (FindObjectsOfType<FlotatingPauseMono>() != null && FindObjectsOfType<FlotatingPauseMono>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Show()
    {
        animator.SetBool(Open, true);
        OnPause?.Invoke(true);
    }

    public void Hide()
    {
        animator.SetBool(Open, false);
        OnPause?.Invoke(false);
    }
}

public interface IFloatingPause
{
    void Show();
    event Action<bool> OnPause;
}