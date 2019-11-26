using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject[] PlaybackCtrls;
    public Slider[] fillSlider;
    public Button[] playBig;

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < PlaybackCtrls.Length; i++)
        {
            PlaybackCtrls[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < playBig.Length; i++)
        {
            playBig[i].GetComponent<Button>().enabled = !videoPlayer.isPlaying;
            playBig[i].GetComponent<Image>().enabled = !videoPlayer.isPlaying;            
        }

		if(videoPlayer.isPlaying)
        {
            float val = videoPlayer.frame / (float)videoPlayer.frameCount;

            for (int i = 0; i < fillSlider.Length; i++)
            {
                fillSlider[i].value = val;
            }

            Color color = videoPlayer.GetComponent<Renderer>().material.color;
            color.a = 1.0f;
            videoPlayer.GetComponent<Renderer>().material.color = color;

            if (Input.GetMouseButtonDown(0))
            {   
                videoPlayer.Pause();
            }
        }
    }

    public void LoadAlbum(string URL)
    {
        videoPlayer.GetComponent<MeshRenderer>().enabled = true;
        for (int i = 0; i < PlaybackCtrls.Length; i++)
        {
            PlaybackCtrls[i].SetActive(true);
        }
        videoPlayer.url = URL;

    }

    public void PlayVideo()
    {
        videoPlayer.Play();        
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void SeekVideoScreen(float seekTo)
    {
        float seekValue = seekTo * videoPlayer.frameCount;

        videoPlayer.frame = (long)seekValue;
        Debug.Log("SeekValue" + seekValue);
        Debug.Log("SeekTo" + seekTo);
    }

    public void SeekVideo(float seekTo)
    {
        float seekValue = seekTo * videoPlayer.frameCount;

        videoPlayer.frame = (long)seekValue;
        Debug.Log("SeekValue" + seekValue);
        Debug.Log("SeekTo" + seekTo);
    }

    public void LoopVideo(bool Loop)
    {
        videoPlayer.isLooping = Loop;
    }
}
