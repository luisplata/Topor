using UnityEngine;

namespace DebugCustom.Script
{
    public class LogError : Log
    {
        public override void Configure(string log, LogType type, int fontSize)
        {
            textDebug.text = log;
            background.color = Color.red;
            textDebug.fontSize = fontSize;
        }
    }
}