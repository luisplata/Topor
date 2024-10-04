using UnityEngine;
using UnityEngine.Events;

public class ActionCustom : MonoBehaviour
{
    [SerializeField] private UnityEvent action;
    
    public void Execute()
    {
        action.Invoke();
    }
}