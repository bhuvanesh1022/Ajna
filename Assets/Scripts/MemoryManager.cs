
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MemoryManager : MonoBehaviour
{
    public VideoPlayer VideoScreen;    
    public GameObject[] PlayButton;
    //public GameObject[] OffersButton;
    //public GameObject[] EnquiryButton;

    public GameObject loading;
    public Image loader;
    public Image loadingFiller;
    public Text loadingText;

    public bool videoPaused;
    
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

        Debug.Log("loading" + isLoading);

        for (int i = 0; i < PlayButton.Length; i++)
        {
            PlayButton[i].SetActive(videoPaused);
        }

        /*
        for (int i = 0; i < OffersButton.Length; i++)
        {
            OffersButton[i].SetActive(videoPaused);
        }

        for (int i = 0; i < EnquiryButton.Length; i++)
        {
            EnquiryButton[i].SetActive(videoPaused);
        }
        */

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

            yield return new WaitForSeconds(0.05f);
        }

        if (www.isDone)
        {
            isLoading = false;            
            Debug.Log("loading" + isLoading);
        }

        yield return www;

        VideoScreen.url = www.url;

        Debug.Log(www.progress);
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
    
    public void OnOffers()
    {
        Debug.Log("Offers Button Clicked");
    }

    public void OnEnquiry()
    {
        Debug.Log("Enquiry Button Clicked");
    }

}
