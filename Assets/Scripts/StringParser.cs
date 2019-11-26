using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringParser : MonoBehaviour
{
    public AppManager InvitationCtrl;
    public AlbumManager AlbumCtrl;
    public BrochureManager BrochureCtrl;
    public MemoryManager MemoryCtrl;
    public DownloadAssetBundleAR RE_AppCtrl;
    public RE_AppManager RE_App;

    public GameObject canvasFor_3d;

    public C_TrackableEventHandler handler;

    public  Renderer videoScreen;
    
    public void Update()
    {
        canvasFor_3d.SetActive(RE_AppCtrl.enabled);
        RE_App.enabled = RE_AppCtrl.enabled;
    }

    public void SplitString(string metadata)
    {
        Debug.Log("Metadata is " + metadata);

        string[] _item = metadata.Split(';');
        
        for (int i = 0; i < _item.Length; i++)
        {
            string[] _list = _item[i].Split(',');
            Debug.Log(_list[0]);

            switch (_list[0])
            {
                case "texture":
                    StartCoroutine(LoadTexture(_list[1]));
                    break;

                case "invitation":
                    handler.AR_Elmnts = C_TrackableEventHandler.arElements.vdo;
                    InvitationCtrl.enabled = true;
                    InvitationCtrl.LoadAR(_list[1]);
                    AlbumCtrl.enabled = false;
                    BrochureCtrl.enabled = false;
                    MemoryCtrl.enabled = false;
                    RE_AppCtrl.enabled = false;
                    Debug.Log(_list[1]);

                    break;

                case "album":
                    handler.AR_Elmnts = C_TrackableEventHandler.arElements.vdo;
                    AlbumCtrl.enabled = true; 
                    AlbumCtrl.LoadAlbum(_list[1]);
                    InvitationCtrl.enabled = false;
                    BrochureCtrl.enabled = false;
                    MemoryCtrl.enabled = false;
                    RE_AppCtrl.enabled = false;
                    Debug.Log(_list[1]);

                    break;

                case "brochure":
                    handler.AR_Elmnts = C_TrackableEventHandler.arElements.vdo;
                    BrochureCtrl.enabled = true;
                    BrochureCtrl.LoadAR(_list[1]);
                    InvitationCtrl.enabled = false;
                    AlbumCtrl.enabled = false;
                    MemoryCtrl.enabled = false;
                    RE_AppCtrl.enabled = false;
                    Debug.Log(_list[1]);

                    break;

                case "memory":
                    handler.AR_Elmnts = C_TrackableEventHandler.arElements.vdo;
                    MemoryCtrl.enabled = true;
                    MemoryCtrl.LoadAR(_list[1]);
                    InvitationCtrl.enabled = false;
                    AlbumCtrl.enabled = false;
                    BrochureCtrl.enabled = false;
                    RE_AppCtrl.enabled = false;
                    Debug.Log(_list[1]);

                    break;

                case "2bhk":
                    handler.AR_Elmnts = C_TrackableEventHandler.arElements.xtrr;
                    RE_AppCtrl.enabled = true;
                    RE_AppCtrl.DownloadAssets(_list[1]);
                    MemoryCtrl.enabled = false;
                    InvitationCtrl.enabled = false;
                    AlbumCtrl.enabled = false;
                    BrochureCtrl.enabled = false;
                    Debug.Log("LoadingRealEstate");

                    break;

                default:
                    break;
            }
        }
    }

    public IEnumerator LoadTexture(string url)
    {
        Color color = videoScreen.material.color;

        Texture2D tex;
        tex = new Texture2D(4, 4);

        WWW www = new WWW(url);
        yield return www;

        www.LoadImageIntoTexture(tex);

        videoScreen.material.mainTexture = tex;

        color.a = 1.0f;
        videoScreen.material.color = color;

    }

}
