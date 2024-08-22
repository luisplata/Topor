using UnityEngine;

public class ConsumableConfig : MonoBehaviour
{
    public void Show()
    {
        ServiceLocator.Instance.GetService<IFloatingConfig>().Show();
    }
}