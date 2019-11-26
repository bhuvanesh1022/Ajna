using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class EnumChooser : MonoBehaviour
{
    public TrackHandlerTest trackHandler;

    public void SetEnum(string data)
    {
        switch (data)
        {
            case "texture":
                trackHandler.AR_Elmnts = TrackHandlerTest.arElements.txtr;
                break;

            case "album":
                trackHandler.AR_Elmnts = TrackHandlerTest.arElements.albm;
                break;

            case "2bhk":
                trackHandler.AR_Elmnts = TrackHandlerTest.arElements.thrD;
                break;

            default:
                break;
        }
        
    }

}
