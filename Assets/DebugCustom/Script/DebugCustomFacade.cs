using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DebugCustom.Script
{
    public class DebugCustomFacade : MonoBehaviour, IDebugCustom
    {
        [SerializeField] private Button button_show_debug;
        [SerializeField] private AnimatorDebugPanel debug_panel_animator;
        [SerializeField] private EventSystem eventSystem;
        private bool isDebugVisible;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            if(FindObjectsOfType<DebugCustomFacade>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            button_show_debug.onClick.AddListener(ShowOrHideDebug);
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("Scene loaded");
            eventSystem.gameObject.SetActive(FindObjectsOfType<EventSystem>().Length < 1);
        }
        
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void ShowOrHideDebug()
        {
            isDebugVisible = !isDebugVisible;
            debug_panel_animator.ShowOrHideDebug(isDebugVisible);
        }
    }
}