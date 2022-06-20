using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }

    public void DisplayVideo(VideoClip videoClip)
    {
        player.Pause();
        player.clip = videoClip;
        player.Play();
    }
}
