using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parser : MonoBehaviour
{
    //public DownloadAssetBundleAR downloadAB;

    public Text txt;    

    public void  SplitMeta(string metadata)
    {
        txt.GetComponent<Text>().text = metadata;
        //downloadAB.DownloadAssets(metadata);

        string[] _item = metadata.Split(';');

        for (int i = 0; i < _item.Length; i++)
        {
            string[] _list = _item[i].Split(',');
            Debug.Log(_list[0]);

            GetComponent<EnumChooser>().SetEnum(_list[0]);
        }
    }
}
