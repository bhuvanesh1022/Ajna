using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RE_AppManager : MonoBehaviour
{
    public Camera AR_Camera;
    public Camera FPS_Camera;

    public C_TrackableEventHandler eventHandler;
    public DownloadAssetBundleAR assetBundleAR;

    public GameObject CameraStagePivot;
    public GameObject obj1;
    public GameObject obj2;

    public Button WT_Button;
    public Button BP_Button;
    public Button Close_Button;

    public void Start()
    {
        //FPS_Camera.enabled = false;
    }

    public void Update()
    {
        if (assetBundleAR.ObjLoaded)
        {
            Close_Button.enabled = true;
            Close_Button.GetComponent<Image>().enabled = true;
            //WT_Button.GetComponent<Image>().enabled = false;
            //BP_Button.GetComponent<Image>().enabled = false;

            if (eventHandler.inInterior)
            {
                BP_Button.GetComponent<Image>().enabled = false;
                BP_Button.GetComponent<Button>().enabled = false;
            }
            else
            {
                if (eventHandler.blueprint)
                {
                    obj1.SetActive(false);
                    obj2.SetActive(true);
                    //WT_Button.SetActive(true);
                    //BP_Button.SetActive(false);
                    WT_Button.GetComponent<Button>().enabled = true;
                    WT_Button.GetComponent<Image>().enabled = true;

                    BP_Button.GetComponent<Image>().enabled = false;
                    BP_Button.GetComponent<Button>().enabled = false;
                }
                else
                {
                    obj1.SetActive(true);
                    obj2.SetActive(false);
                    //WT_Button.SetActive(false);
                    //BP_Button.SetActive(true);
                    WT_Button.GetComponent<Button>().enabled = true;
                    WT_Button.GetComponent<Image>().enabled = true;

                    BP_Button.GetComponent<Image>().enabled = true;
                    BP_Button.GetComponent<Button>().enabled = true;
                }
            }
        }   
        else
        {
            WT_Button.GetComponent<Button>().enabled = false;
            WT_Button.GetComponent<Image>().enabled = false;

            BP_Button.GetComponent<Image>().enabled = false;
            BP_Button.GetComponent<Button>().enabled = false;
        }
        
    }

    public void ToBluePrint(string blueprint)
    {
        if (!eventHandler.inInterior)
        {
            switch (blueprint)
            {
                case "blueprint":
                    eventHandler.blueprint = true;
                    CameraStagePivot.transform.rotation = Quaternion.Euler(new Vector3(-45.0f, 0.0f, 0.0f));
                    //CameraStagePivot.GetComponent<LeanRotateCustomAxis>().Axis = Vector3.right;
                    break;

                case "walkthrough":
                    if (eventHandler.blueprint)
                    {
                        eventHandler.blueprint = false;
                        CameraStagePivot.transform.rotation = Quaternion.Euler(new Vector3(-10.0f, 0.0f, 0.0f));
                        //CameraStagePivot.GetComponent<LeanRotateCustomAxis>().Axis = Vector3.forward;
                    }
                    else
                    {
                        OnTap();
                        Debug.Log("ON tap Called");
                    }             
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (blueprint)
            {
                case "walkthrough":
                    OnTap();
                    break;
                default:
                    break;
            }
        }
    }

    public void ReloadApp()
    {
        Caching.ClearCache();
        SceneManager.LoadScene(0);
        
    }

    public void OffRef()
    {
        if (!eventHandler.blueprint)
        {
            CameraStagePivot.GetComponent<LeanScale>().enabled = true;
            CameraStagePivot.GetComponent<LeanTranslate>().enabled = true;
            CameraStagePivot.GetComponent<LeanRotateCustomAxis>().enabled = true;
        }
        else
        {
            CameraStagePivot.GetComponent<LeanScale>().enabled = false;
            CameraStagePivot.GetComponent<LeanTranslate>().enabled = false;
            CameraStagePivot.GetComponent<LeanRotateCustomAxis>().enabled = false;
        }
        
    }

    public void OnTap()
    {
        if (!eventHandler.blueprint)
        {
            if (!eventHandler.inInterior)
            {
                AR_Camera.enabled = false;
                FPS_Camera.enabled = true;
                eventHandler.inInterior = true;
                FPS_Camera.GetComponent<GyroControl>().enabled = true;
                CameraStagePivot.GetComponent<LeanScale>().enabled = false;
                CameraStagePivot.GetComponent<LeanTranslate>().enabled = false;
                CameraStagePivot.GetComponent<LeanRotateCustomAxis>().enabled = false;
                Debug.Log("Double Tapped");
            }
            else
            {
                AR_Camera.enabled = true;
                FPS_Camera.enabled = false;
                eventHandler.inInterior = false;
                FPS_Camera.GetComponent<GyroControl>().enabled = false;
                CameraStagePivot.GetComponent<LeanScale>().enabled = true;
                CameraStagePivot.GetComponent<LeanTranslate>().enabled = true;
                CameraStagePivot.GetComponent<LeanRotateCustomAxis>().enabled = true;
            }
        }
        
    }


}
