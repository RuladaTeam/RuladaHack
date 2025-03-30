using Core.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class LoadMenuButtonSonocName : MonoBehaviour
{
    public void OnClick()
    {
        VideoPlayer videoPlayer = GameObject.FindWithTag("Videoplayer").GetComponent<VideoPlayer>();    
        videoPlayer.url = "http://localhost:3000/sonography/converted/" + UltrasonographyTabs.Instance._currentPatient + "/" + gameObject.GetComponentInChildren<TextMeshProUGUI>().text; 
        videoPlayer.Play();
    }
}
