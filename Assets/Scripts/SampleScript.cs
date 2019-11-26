using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    public GameObject iris;
    public float speed;
    public Vector3 temp;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        iris.transform.Rotate(0,0, Time.deltaTime * speed);
	}
}
