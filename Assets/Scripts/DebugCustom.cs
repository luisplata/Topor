using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debug;

    private void Start()
    {
        Application.logMessageReceived += Handle_Logs;
    }

    public void Handle_Logs(string logString, string stackTrace, LogType type)
    {
        var format = $"{type}: {logString}";
        if (type == LogType.Error || type == LogType.Exception)
        {
            format += $"\n{stackTrace}";
        }

        format += "\n\n";
        debug.text += format;
    }
}