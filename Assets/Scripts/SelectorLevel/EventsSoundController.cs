using System;
using UnityEngine;
using UnityEngine.UI;

public class EventsSoundController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    public bool isScrolling;
    public float velocity;

    private void Update()
    {
        if (isScrolling)
        {
            velocity = scrollRect.velocity.x;
            if (velocity is > 0.1f and < 1f)
            {
                Debug.Log($"burrrrr");
            }
        }
    }

    public void OnScrollStart()
    {
        isScrolling = true;
    }

    public void OnScrollEnd()
    {
        isScrolling = false;
        Debug.Log($"Release");
    }
}