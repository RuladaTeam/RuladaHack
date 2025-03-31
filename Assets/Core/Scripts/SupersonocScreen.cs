using UnityEngine;
using UnityEngine.Video;

public class SupersonocScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    
    public void OnClick()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else if (videoPlayer.url != "")
        {
            videoPlayer.Play();
        }
    }
}
