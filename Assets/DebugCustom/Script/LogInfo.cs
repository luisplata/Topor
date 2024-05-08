using UnityEngine;

namespace DebugCustom.Script
{
    public class LogInfo : Log
    {
        public override void Configure(string log, LogType type)
        {
            base.Configure(log, type);
            background.color = Color.yellow;
        }
    }
}