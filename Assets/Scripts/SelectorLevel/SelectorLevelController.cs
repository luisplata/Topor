using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectorLevelController : MonoBehaviour, ISelectorLevelController
{
    [SerializeField] List<LevelController> levels;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoClipStart;
    [SerializeField] private GameObject video;
    [SerializeField] private Button skipButton;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ISelectorLevelController>(this);
    }

    private void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Videos", videoClipStart + ".mp4");
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += source =>
        {
            video.SetActive(true);
            videoPlayer.Play();
            StartCoroutine(FinishVideo());
        };
        skipButton.onClick.AddListener(() =>
        {
            videoPlayer.Stop();
            video.SetActive(false);
        });
    }

    private IEnumerator FinishVideo()
    {
        yield return new WaitForSeconds((float)videoPlayer.length);
        videoPlayer.Stop();
        video.SetActive(false);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ISelectorLevelController>();
    }
}

public interface ISelectorLevelController
{
}