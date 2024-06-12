using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeLightsSystem : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color minColor, maxColor;
    [SerializeField] private GameObject pointA, pointB;
    private ITimeLineService _logicOfLevel;
    
    public void Configure(ITimeLineService timeLineService)
    {
        _logicOfLevel = (LogicOfLevel) timeLineService;
    }
    
    public void SetInterval(float percent)
    {
        globalLight.gameObject.transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, percent);
        if (percent is >= 0 and < 0.5f)
        {
            //para el de 0 a 0.5 es de 0 a 1
            percent *= 2;
            globalLight.color = Color.Lerp(minColor, maxColor, percent);
        }
        else
        {
            //para el de 0.5 a 1 es de 0 a 1
            percent = percent * 2 - 1;
            globalLight.color = Color.Lerp(maxColor, minColor, percent);
        }
    }
}