using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugCustom.Script
{
    public class Log : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textDebug;
        [SerializeField] protected Image background;

        public virtual void Configure(string log, LogType type, int fontSize)
        {
            textDebug.text = log;
            textDebug.fontSize = fontSize;
        }
    }
}