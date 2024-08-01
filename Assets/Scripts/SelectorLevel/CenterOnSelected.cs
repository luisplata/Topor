using UnityEngine;
using UnityEngine.UI;

public class CenterOnSelected : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform target;
    public RectTransform center;

    public void CenterOn()
    {
        Vector2 position = (Vector2)scrollRect.transform.InverseTransformPoint(target.position)
                           - (Vector2)scrollRect.transform.InverseTransformPoint(center.position);
        
        position.x = (position.x / scrollRect.content.rect.width) + 0.5f;
        position.y = (position.y / scrollRect.content.rect.height) + 0.5f;
        
        scrollRect.horizontalNormalizedPosition = position.x;
        scrollRect.verticalNormalizedPosition = position.y;
    }
}