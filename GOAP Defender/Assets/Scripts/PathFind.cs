using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFind : MonoBehaviour {


    NavMeshAgent me;
    public Transform target;

    public Library lib;

	// Use this for initialization
	void Start () {
        me = gameObject.GetComponent<NavMeshAgent>();
        ChangeDoor(lib.door["DoorD"], true);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        me.SetDestination(target.position);


    }

    void ChangeDoor(int door, bool open)
    {
        int value = (int)Mathf.Pow(2, door + 3);
        me.areaMask += open? value:-value;
        //area mask works by converting the int value to binary and using it as bolleans for each layer on the NavMeshAgent AreaMaks componnent.
    }

}
