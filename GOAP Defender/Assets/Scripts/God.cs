using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    public GameObject[] doors;
    public bool[] doorStates;
    void Awake()
    {
        doorStates = new bool[] { true, true, true, true };
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void breakDoor(int door)
    {
        doors[door].SetActive(false);
        doorStates[door] = false;
    } 
    public void fixDoor(int door)
    {
        doors[door].SetActive(true);
        doorStates[door] = true;
    }

}
