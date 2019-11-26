using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationsManager : MonoBehaviour
{

    public GameObject ImageTarget;
    public GameObject camStage;
    public GameObject AR_Stage;
    public GameObject AugmentableObj;


    public bool moveToAR;
    public bool moveToCam;

    // Use this for initialization
    void Start()
    {
        camStage.SetActive(false);
        moveToAR = false;
        moveToCam = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToAR)
        {
            Quaternion tempRot = ImageTarget.transform.rotation;
            Quaternion newRot = Quaternion.Euler(tempRot.x, tempRot.y + 180, tempRot.z);

            AugmentableObj.transform.position = Vector3.Lerp(AugmentableObj.transform.position, ImageTarget.transform.position, 5.0f * Time.deltaTime);
            AugmentableObj.transform.rotation = Quaternion.Slerp(AugmentableObj.transform.rotation, ImageTarget.transform.rotation, 5.0f * Time.deltaTime);
        }

        if (moveToCam)
        {
            AugmentableObj.transform.position = Vector3.Lerp(AugmentableObj.transform.position, new Vector3(-0.02f, -0.2f, -0.15f ), 5.0f * Time.deltaTime);
            //AugmentableObj.transform.rotation = Quaternion.Slerp(AugmentableObj.transform.rotation, Quaternion.Euler(Vector3.right * 90), 5.0f * Time.deltaTime);
            //AugmentableObj.transform.rotation = Quaternion.Slerp(AugmentableObj.transform.rotation, Quaternion.identity, 5.0f * Time.deltaTime);
            AugmentableObj.transform.rotation = Quaternion.Slerp(AugmentableObj.transform.rotation, Quaternion.Euler(new Vector3(-90.0f, 90.0f, 90.0f)), 5.0f * Time.deltaTime);

            float dist = Vector3.Distance(AugmentableObj.transform.position, new Vector3(-0.02f, -0.2f, -0.15f ));

            if ( dist <= 0.1f)
            {
                AugmentableObj.transform.position = new Vector3(-0.02f, -0.2f, -0.15f );
                //AugmentableObj.transform.rotation = Quaternion.Euler(Vector3.right * 90);
                AugmentableObj.transform.rotation = Quaternion.Euler(new Vector3(-90.0f, 90.0f, 90.0f));
                moveToCam = false;
            }
        }
    }

    public void OffTheReference(GameObject Obj)
    {
        moveToAR = false;
        moveToCam = true;

        camStage.SetActive(true);
        Obj.transform.parent = camStage.transform;
        //Obj.transform.position = Vector3.zero;
        //Obj.transform.rotation = Quaternion.Euler(Vector3.right * 90);
        //Obj.transform.rotation = Quaternion.identity;
        //Obj.transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);
        Obj.transform.localScale = Vector3.one;
    }

    public void OnTheReference(GameObject Obj)
    {
        ImageTarget.transform.position = Vector3.zero;
        ImageTarget.transform.rotation = Quaternion.identity;
        moveToAR = true;

        //Obj.transform.position = ImageTarget.transform.position;
        //Obj.transform.rotation = ImageTarget.transform.rotation;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.parent = AR_Stage.transform;

        camStage.transform.position = Vector3.zero;
        camStage.transform.rotation = Quaternion.identity;
        camStage.SetActive(false);
        Debug.Log("Parented to AR_Stage");
    }
}
