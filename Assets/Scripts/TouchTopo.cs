using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TouchTopo : MonoBehaviour
{
    public UnityEvent OnTouchTopo;
    public UnityEvent OnTouchScreen;
    private Vector2 point;
    private Topo currentTopo;
    private bool _canPlaySounds;

    public void OnTouch(InputAction.CallbackContext context)
    {
        //need the events to press and release the button
        if (context.phase == InputActionPhase.Started)
        {
            
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentTopo = ShotRayToTopo();
            if (currentTopo != null)
            {
                ServiceLocator.Instance.GetService<IAnimationBehaviour>().PlaySuccessHit();
                currentTopo?.Touch();
                if (_canPlaySounds)
                {
                    OnTouchTopo.Invoke();
                }
            }
            else
            {
                ServiceLocator.Instance.GetService<IAnimationBehaviour>().PlayFailHit();
                if (_canPlaySounds)
                {
                    OnTouchScreen?.Invoke();
                }
            }
        }
    }
    
    public void OnPoint(InputAction.CallbackContext context)
    {
        point = context.ReadValue<Vector2>();
    }


    private Topo ShotRayToTopo()
    {
        Vector2 touchPosition = point;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            return hit.collider.gameObject.GetComponent<Topo>();
        }

        return null;
    }

    public void CanPlaySounds(bool canPlay)
    {
        _canPlaySounds = canPlay;
    }
}
