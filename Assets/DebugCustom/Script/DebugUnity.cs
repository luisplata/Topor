using System;
using TMPro;
using UnityEngine;

namespace DebugCustom.Script
{
    public class DebugUnity : MonoBehaviour
    {
        [SerializeField] private Log log, logError, logWarning;
        [SerializeField] private GameObject panelDebug;

        private void Awake()
        {
            Application.logMessageReceived += Handle_Logs;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= Handle_Logs;
        }

        private void Handle_Logs(string logString, string stackTrace, LogType type)
        {
            var textToLog = $"{type}: {logString}";
            if (type == LogType.Error || type == LogType.Exception)
            {
                textToLog += $"\n{stackTrace}";
            }

            switch (type)
            {
                case LogType.Error:
                case LogType.Exception:
                    CreateLog(logError, textToLog, type);
                    break;
                case LogType.Warning:
                    CreateLog(logWarning, textToLog, type);
                    break;
                default:
                    CreateLog(log, textToLog, type);
                    break;
            }
        }

        private void CreateLog(Log logPrefab, string textToLog, LogType type)
        {
            Log newLog = Instantiate(logPrefab, panelDebug.transform);
            newLog.Configure(textToLog, type);
        }
    }
}