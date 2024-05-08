using UnityEngine;

namespace DebugCustom.Script
{
    public class LogInfo : Log
    {
        public override void Configure(string log, LogType type, int fontSize)
        {
            textDebug.text = log;
            background.color = Color.yellow;
            textDebug.fontSize = fontSize;
        }
    }
}