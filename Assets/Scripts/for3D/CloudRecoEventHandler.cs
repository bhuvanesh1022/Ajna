using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CloudRecoEventHandler : MonoBehaviour, IObjectRecoEventHandler
{

    public ImageTargetBehaviour targetBehaviour;
    public Parser parser;

    private CloudRecoBehaviour mCloudRecoBehaviiour;

    private bool mIsScanning = false;
    private string mTargetMetadata = " ";

    // Use this for initialization
    void Start()
    {
        mCloudRecoBehaviiour = GetComponent<CloudRecoBehaviour>();
        if (mCloudRecoBehaviiour)
        {
            mCloudRecoBehaviiour.RegisterEventHandler(this);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco Init error " + initError.ToString());
    }

    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco Initialized");
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        TargetFinder.CloudRecoSearchResult cloudRecoSearchResult = (TargetFinder.CloudRecoSearchResult)targetSearchResult;
        mIsScanning = false;
        mTargetMetadata = cloudRecoSearchResult.MetaData;
        Debug.Log(mTargetMetadata);

        if(targetBehaviour)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.GetTargetFinder<TargetFinder>().EnableTracking(targetSearchResult, targetBehaviour.gameObject);
        }

        parser.SplitMeta(mTargetMetadata);

    }

    public void OnStateChanged(bool scanning)
    {
        Debug.Log("<color=blue>OnStateChanged(): </color>" + scanning);

        mIsScanning = scanning;

        Debug.Log("<color=blue>mIsScanning: </color>" + mIsScanning);

        if(scanning)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<TargetFinder>().ClearTrackables(false);
        }
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    void Hide(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
}
