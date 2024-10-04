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
        DontDestroyOnLoad(gameObject);
    }
}