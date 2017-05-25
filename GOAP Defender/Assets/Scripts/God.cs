using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    public GameObject[] doors;
    public bool[] doorStates;
    public float distFactor;
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
        print("Door " + door + " broke!!!");
    } 
    public void fixDoor(int door)
    {
        doors[door].SetActive(true);
        doorStates[door] = true;
        //print("Door " + door + " is fixed!!!");
    }

    public void loseGame()
    {
        print("<color=red> PEDEU!!!!!!!!!!!!!!!!!!!! </color>");
        Application.Quit();
    }

}
