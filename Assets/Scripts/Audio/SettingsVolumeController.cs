using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Topor.Audio
{
    public class SettingsVolumeController : MonoBehaviour
    {
        [SerializeField] private bool useBusNotVca;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private string busToControl;
        [SerializeField] private string playerPrefKey;
        private void OnEnable()
        {
            volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
        }

        private void OnDisable()
        {
            volumeSlider.onValueChanged.RemoveListener(HandleSliderValueChanged);
        }

        private void Start()
        {
            float currentVolume;
            
            if (!string.IsNullOrEmpty(busToControl))
            {
                if (useBusNotVca)
                {
                    RuntimeManager.GetBus(busToControl).getVolume(out currentVolume);
                }
                else
                {
                    RuntimeManager.GetVCA(busToControl).getVolume(out currentVolume);
                }
                volumeSlider.value = PlayerPrefs.GetFloat(playerPrefKey, currentVolume);
            }
            else
            {
                Debug.Log("bus not assigned");
            }
        }

        private void HandleSliderValueChanged(float value)
        {
            if (!string.IsNullOrEmpty(busToControl))
            {
                PlayerPrefs.SetFloat(playerPrefKey, value);
                if (useBusNotVca)
                {
                    RuntimeManager.GetBus(busToControl).setVolume(value);
                }
                else
                {
                    RuntimeManager.GetVCA(busToControl).setVolume(value);
                }
            }
            else
            {
                Debug.Log("bus not assigned");
            }
        }
    }
}
