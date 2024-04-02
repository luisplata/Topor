using UnityEngine;
using UnityEngine.InputSystem;

public class TouchTopo : MonoBehaviour
{
    [SerializeField] private DebugCustom debugCustom;
    private Vector2 point;
    public void OnTouch(InputAction.CallbackContext context)
    {
        //need the events to press and release the button
        if (context.phase == InputActionPhase.Started)
        {
            debugCustom.DebugText("Pressing Topo");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            debugCustom.DebugText("Releasing Topo");
            Vector2 touchPosition = point;
            Debug.Log("Touch position: " + touchPosition);
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                GameObject touchedObject = hit.transform.gameObject;
                debugCustom.DebugText("Touched object: " + touchedObject.name);
            }
            
        }
        else
        {
            debugCustom.DebugText("Touching Topo");
        }
    }
    
    public void OnPoint(InputAction.CallbackContext context)
    {
        point = context.ReadValue<Vector2>();
    }
}
