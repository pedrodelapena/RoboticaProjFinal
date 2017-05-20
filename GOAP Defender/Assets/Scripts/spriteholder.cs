using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteholder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(90, 0, 0);
        //transform.Rotate(Vector3.right, 90);
	}
}
