using UnityEngine;

public class ConsumablePause : MonoBehaviour
{
    public void Show()
    {
        ServiceLocator.Instance.GetService<IFloatingPause>().Show();
    }
}
