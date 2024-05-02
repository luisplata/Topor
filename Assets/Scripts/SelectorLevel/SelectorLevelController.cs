using System.Collections.Generic;
using UnityEngine;

public class SelectorLevelController : MonoBehaviour, ISelectorLevelController
{
    [SerializeField] List<LevelController> levels;
    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ISelectorLevelController>(this);
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ISelectorLevelController>();
    }
}

public interface ISelectorLevelController
{
}
