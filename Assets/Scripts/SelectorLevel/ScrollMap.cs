using UnityEngine;
using UnityEngine.UI;

public class ScrollMap : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    private float _maxVelocity = float.MinValue;


    public void OnChangeValue(Vector2 floatValue)
    {
        var forceToScroll = Mathf.Abs(scrollRect.velocity.x);
        if (forceToScroll > _maxVelocity)
        {
            _maxVelocity = forceToScroll;
        }
        
        Debug.Log($"ScrollRect: {forceToScroll} MaxVelocity: {_maxVelocity} - normalize 0-1: {forceToScroll / _maxVelocity}");
    }
}