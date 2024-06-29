using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerScreen : MonoBehaviour
{
    public UnityEvent OnFinishVideo;
    [SerializeField] private VideoPlayer videoPlayer;
    
    private void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += source =>
        {
            videoPlayer.Play();
            StartCoroutine(FinishVideo());
        };
    }

    private IEnumerator FinishVideo()
    {
        yield return new WaitForSeconds((float)videoPlayer.length);
        videoPlayer.Stop();
        OnFinishVideo.Invoke();
    }
}