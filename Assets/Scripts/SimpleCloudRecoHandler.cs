using UnityEngine;
using Vuforia;

public class SimpleCloudRecoHandler : MonoBehaviour, IObjectRecoEventHandler
{
    public ImageTargetBehaviour targetBehaviour;

    public GameObject MainVideoPlayer;
    public StringParser stringParser;
    //public GameObject scanningIMGo;
    //public GameObject scanningIMG;
    public GameObject scanningPanel;
    //public GameObject metadata;

    private CloudRecoBehaviour mCloudRecoBehaviour;

    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    // Use this for initialization
    void Start()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterEventHandler(this);
        }
                
        Hide(MainVideoPlayer);
                                                                                                 
    }

    public void Update()
    {
        scanningPanel.SetActive(mIsScanning);
        
        //scanningIMG.transform.Rotate(0.0f, 0.0f, Time.deltaTime * 200.0f);
        //scanningIMGo.transform.Rotate(0.0f, 0.0f, Time.deltaTime * -100.0f);
        
        //if (mCloudRecoBehaviour && mCloudRecoBehaviour.CloudRecoEnabled)
        //{   

        //    Debug.Log(mCloudRecoBehaviour.CloudRecoEnabled);          
        //}
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

        //GameObject newImageTarget = Instantiate(targetBehaviour.gameObject) as GameObject;

        //MainVideoPlayer = newImageTarget.transform.GetChild(0).gameObject;
        //metadata = newImageTarget.transform.GetChild(1).gameObject;
        //Debug.Log(newImageTarget.transform.GetChild(1).transform.GetChild(0));

        //GameObject augmentation = null;
        //if (augmentation != null)
        //{
        //    augmentation.transform.SetParent(newImageTarget.transform);
        //    Debug.Log("Augmentation is " + augmentation.name);
        //}

        if (targetBehaviour)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, targetBehaviour.gameObject);
        }

        //MainVideoPlayer.GetComponent<VideoPlayer>().url = mTargetMetadata.Trim();
        //appManager.LoadAR(mTargetMetadata.Trim());
        stringParser.SplitString(mTargetMetadata.Trim());
        //metadata.GetComponentInChildren<Text>().text = mTargetMetadata;
        if (!mIsScanning)
        {
            mCloudRecoBehaviour.CloudRecoEnabled = true;
        }
    }

    public void OnStateChanged(bool scanning)
    {
        Debug.Log("<color=blue>OnStateChanged(): </color>" + scanning);

        mIsScanning = scanning;
        Debug.Log("<color=blue>mScanning: </color>" + mIsScanning);

        if (scanning)
        {   
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    
	
	// Update is called once per frame
	void Hide (GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        foreach (var item in renderers)
        {
            item.enabled = false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

	}

}
