using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventsSoundController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    public UnityEvent OnScroll;
    public bool isScrolling;
    public float velocity;
    public float maxVelocity;
    private bool _canPlaySounds = true;

    private void Update()
    {
        if (isScrolling)
        {
            velocity = Mathf.Abs(scrollRect.velocity.x);
            var normalizedVelocityWithClamp = Mathf.Clamp(velocity / maxVelocity, 0, 1);
            Debug.Log($"Scrolling: {normalizedVelocityWithClamp}");
            if (normalizedVelocityWithClamp is > 0.1f and <= 1f && _canPlaySounds)
            {
                OnScroll?.Invoke();
                _canPlaySounds = false;
            }
            else if(normalizedVelocityWithClamp == 0)
            {
                _canPlaySounds = true;
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
        _canPlaySounds = true;
        Debug.Log($"Release");
    }
}