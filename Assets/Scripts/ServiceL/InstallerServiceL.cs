using MenuUI.SystemOfExtras;
using ServiceLocatorPath;
using UnityEngine;

public class InstallerServiceL : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<InstallerServiceL>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        ServiceLocator.Instance.RegisterService<ISaveDataToLevels>(new SaveDataToLevels());
        var p = new PlayFabCustom();
        ServiceLocator.Instance.RegisterService<IPlayFabCustom>(p);
        ServiceLocator.Instance.RegisterService<ISaveData>(p);
        DontDestroyOnLoad(gameObject);
    }
}