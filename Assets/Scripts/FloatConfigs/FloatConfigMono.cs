using System;
using UnityEngine;

public class FloatConfigMono : MonoBehaviour, IFloatingConfig
{
    [SerializeField] private Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Start()
    {
        if (FindObjectsOfType<FloatConfigMono>() != null && FindObjectsOfType<FloatConfigMono>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        ServiceLocator.Instance.RegisterService<IFloatingConfig>(this);
        DontDestroyOnLoad(gameObject);
    }

    public void Show()
    {
        animator.SetBool(Open, true);
    }
    
    public void Hide()
    {
        animator.SetBool(Open, false);
    }
}

public interface IFloatingConfig
{
    void Show();
}