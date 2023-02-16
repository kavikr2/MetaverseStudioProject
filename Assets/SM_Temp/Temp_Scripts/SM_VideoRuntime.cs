using UnityEngine;
using UnityEngine.Video;

public class SM_VideoRuntime : MonoBehaviour
{
    private VideoPlayer MyVideoPlayer;

    private void Start()
    {
        MyVideoPlayer = GetComponent<VideoPlayer>();
        // play video player
        MyVideoPlayer.Play();
    }
}