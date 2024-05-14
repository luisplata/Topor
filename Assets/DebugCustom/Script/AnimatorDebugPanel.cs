using UnityEngine;

namespace DebugCustom.Script
{
    public class AnimatorDebugPanel : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void ShowOrHideDebug(bool isDebugVisible)
        {
            animator.SetBool("IsVisible", isDebugVisible);
        }
    }
}