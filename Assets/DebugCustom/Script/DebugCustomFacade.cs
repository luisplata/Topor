using UnityEngine;
using UnityEngine.UI;

namespace DebugCustom.Script
{
    public class DebugCustomFacade : MonoBehaviour, IDebugCustom
    {
        [SerializeField] private Button button_show_debug;
        [SerializeField] private AnimatorDebugPanel debug_panel_animator;
        private bool isDebugVisible;

        private void Awake()
        {
            if(FindObjectsOfType<DebugCustomFacade>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            button_show_debug.onClick.AddListener(ShowOrHideDebug);
        }

        private void ShowOrHideDebug()
        {
            isDebugVisible = !isDebugVisible;
            debug_panel_animator.ShowOrHideDebug(isDebugVisible);
        }
    }
}