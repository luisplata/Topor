using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugCustom.Script
{
    public class Log : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textDebug;
        [SerializeField] protected Image background;
        [SerializeField] protected float fontSize;
        

        public virtual void Configure(string log, LogType type)
        {
            textDebug.text = log;
            textDebug.fontSize = fontSize;
        }
    }
}