using FMOD.Studio;
using UnityEngine;
using FMODUnity;

namespace Topor.Audio
{
    [System.Serializable]
    public class InitializeFMODSettings : MonoBehaviour
    {
        
        [SerializeField] private string busName;
        [Range(0f, 1f)]
        [SerializeField] private float defaultVolume = 0.75f;
        [SerializeField] private string volumePrefKey;

        void Awake()
        {
            InitializeVolume();
        }

        private void InitializeVolume()
        {
            if (defaultVolume == 0f)
            {
                Debug.Log($"Default value to 0. No sound will be played on {busName} until changed!");
            } 
            float volume = PlayerPrefs.GetFloat(volumePrefKey, defaultVolume);
            
            if (string.IsNullOrEmpty(busName))
            {
                Debug.Log("bus not assigned on " + gameObject.name);
                return;
            }
            VCA bus = RuntimeManager.GetVCA(busName);
            
            bus.setVolume(volume);
        }

        private void OnApplicationQuit()
        {
            SaveVolumeSettings();
        }

        private void SaveVolumeSettings()
        {
            var bus = RuntimeManager.GetVCA(busName);
            bus.getVolume(out float volume);
            PlayerPrefs.SetFloat(volumePrefKey, volume);
        }
    }
}