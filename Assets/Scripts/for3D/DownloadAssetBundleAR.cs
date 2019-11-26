using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DownloadAssetBundleAR : MonoBehaviour
{   
    //public string path;
    public string obj1;
    public string obj2;
    public GameObject AugmentingObj;
    public RE_AppManager appManager;
    public Image loadingImage;
    public bool ObjLoaded = false;
    public GameObject videoPlayer;

    AssetBundle bundle;
    GameObject model1;
    GameObject model2;
    Camera fpsCamera;
    UnityWebRequest request;
    bool isRunning = false;

    public void Update()
    {
        loadingImage.enabled = isRunning;
        loadingImage.GetComponent<Transform>().Rotate(Vector3.forward, -250.0f * Time.deltaTime );
        if (isRunning)
        {
            //Debug.Log(request.downloadProgress);
        }
    }

    public void DownloadAssets(string path)
    {
        videoPlayer.SetActive(false);
        isRunning = true;
        StartCoroutine(DownloadAssetBundles(path));
        Debug.Log("started");
    }

    IEnumerator DownloadAssetBundles(string bundleUrl)
    {
        request = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl, 0);
        yield return request.SendWebRequest();

        isRunning = false;
        Debug.Log(isRunning);
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        Debug.Log(bundle.name);
        string[] assets = bundle.GetAllAssetNames();

        for (int i = 0; i < assets.Length; i++)
        {
            Debug.Log(assets[i]);
        }

        model1 = Instantiate(bundle.LoadAsset<GameObject>(obj1), AugmentingObj.transform);
        appManager.obj1 = model1;
        appManager.FPS_Camera = model1.GetComponentInChildren<Camera>();

        model2 = Instantiate(bundle.LoadAsset<GameObject>(obj2), AugmentingObj.transform);
        appManager.obj2 = model2;

        ObjLoaded = true;
        Debug.Log(bundle != null ? "Asset Bundle Loaded" : "Bundle Not Loaded");
    }
}
