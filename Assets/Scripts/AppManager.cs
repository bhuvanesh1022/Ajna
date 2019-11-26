
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public VideoPlayer VideoScreen;    
    public GameObject[] PlayButton;
    public GameObject[] DateButton;
    public GameObject[] LocationButton;
    
    public GameObject loading;
    public Image loader;
    public Image loadingFiller;
    public Text loadingText;

    //public double time;
    //public double currentTime;

    public bool videoPaused;
    

    //public float lati;
    //public float longi;
    //public string place;
    public float year;
    public float month;
    public float date;
    
    private bool isLoading;

    // Use this for initialization
    void Start ()
    {
        isLoading = false;
        videoPaused = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        loading.SetActive(isLoading);

        if(isLoading)
        {
            loader.transform.Rotate(0, 0, Time.deltaTime * -100);
            loadingFiller.transform.Rotate(0, 0, Time.deltaTime * -25);
        }

        //LoadingText.GetComponent<Text>().enabled = isLoading;
        //loadingSlider.SetActive(isLoading);

        //loadingSlider.SetActive(isLoading);
        //LoadingText.enabled = isLoading;
        Debug.Log("loading" + isLoading);
        

        for (int i = 0; i < PlayButton.Length; i++)
        {
            PlayButton[i].SetActive(videoPaused);
        }

        for (int i = 0; i < DateButton.Length; i++)
        {
            DateButton[i].SetActive(videoPaused);
        }

        for (int i = 0; i < LocationButton.Length; i++)
        {
            LocationButton[i].SetActive(videoPaused);
        }

        if (VideoScreen.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                videoPaused = true;
                VideoScreen.Pause();
            }

        }

        VideoScreen.loopPointReached += EndReached;

    }

    public void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.frame = 50;
        vp.Pause();
        videoPaused = true;
    }

    public void LoadAR(string meta)
    {
        //VideoScreen.url = meta;
        VideoScreen.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(LoadVideo(meta));
    }

    
    public IEnumerator LoadVideo(string URL)
    {
        WWW www = new WWW(URL);
        Debug.Log(www.isDone);
        Debug.Log(www.progress);

        while (!www.isDone)
        {
            isLoading = true;

            loadingFiller.GetComponent<Image>().fillAmount = www.progress;

            float progress = Mathf.Round(www.progress * 100);
            string Loadingtext = progress + "%";
            //loadingText.GetComponent<Text>().text = Loadingtext;
            Debug.Log(loadingText);

            //if (progress <= 20.0f)
            //{

            //}

            //if (progress >= 20.1f && progress <= 60.0f)
            //{
            //    string loadingText = "Recording Video " + progress + "%";
            //    //LoadingText.GetComponent<Text>().text = loadingText;
            //    Debug.Log(loadingText);
            //}

            //if (progress >= 60.1f && progress <= 100.0f)
            //{
            //    string loadingText = "Processing Video to AR " + progress + "%";
            //    //LoadingText.GetComponent<Text>().text = loadingText;
            //    Debug.Log(loadingText);
            //}

            //loadingSlider.GetComponent<Slider>().value = www.progress;
            //loadingSlider.GetComponentInChildren<Slider>().value = www.progress;

            yield return new WaitForSeconds(0.05f);
        }

        if (www.isDone)
        {
            isLoading = false;            
            Debug.Log("loading" + isLoading);
        }

        yield return www;

        VideoScreen.url = www.url;
        //time = VideoScreen.clip.length;

        Debug.Log(www.progress);
    }


        public void QuitApp()
    {
        Application.Quit();
    }
    
    public void ReloadApp()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayVideo()
    {
        VideoScreen.Play();
        videoPaused = false;
    }

    public void PauseVideo()
    {
        VideoScreen.Pause();
    }

    public void StopVideo()
    {
        VideoScreen.Stop();
    }

    public void LoopVideo(bool Loop)
    {
        VideoScreen.isLooping = Loop;
    }

    public void SeekVideo(float seekTo)
    {
        float seekValue = seekTo * VideoScreen.frameCount;
        
        VideoScreen.frame = (long)seekValue;
        Debug.Log("SeekValue" + seekValue);
        Debug.Log("SeekTo" + seekTo);
    }

    public void SaveTheDate()
    {
        //string date = "http://calendar.google.com/calendar/r/day/2019/1/1";
        string day = "http://calendar.google.com/calendar/r/day/" + year + "/" + month + "/" + date;
        Application.OpenURL(day);
    }

    public void OnMap()
    {
        //string url = "http://maps.google.com/maps?q=" + lati + "," + longi;
        //string url = "http://maps.google.com/maps/place/" + place + "/@" + lati + "," + longi;
        //Debug.Log(url);
        //Application.OpenURL("http://maps.google.com/maps?q=13.0183711,80.241");
        Application.OpenURL("https://www.google.com/maps/place/Ayswariya+Mahal/@13.0669201,80.2070722,17z/data=!3m1!4b1!4m12!1m6!3m5!1s0x3a5266a7de03566f:0x1403c6057905c77d!2sAyswariya+Mahal!8m2!3d13.0669201!4d80.2092609!3m4!1s0x3a5266a7de03566f:0x1403c6057905c77d!8m2!3d13.0669201!4d80.2092609");
    }

}
