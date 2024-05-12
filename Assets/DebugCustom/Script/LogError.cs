using UnityEngine;

namespace DebugCustom.Script
{
    public class LogError : Log
    {
        public override void Configure(string log, LogType type)
        {
            base.Configure(log, type);
            background.color = Color.red;
        }
    }
}