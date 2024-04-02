using System;
using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour, IDebugCustom
{
    [SerializeField] private TextMeshProUGUI debugText;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IDebugCustom>(this);   
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IDebugCustom>();
    }

    public void DebugText(string text)
    {
        debugText.text += text + "\n";
        Debug.Log(text);
    }
}

public interface IDebugCustom
{
    void DebugText(string text);
}
