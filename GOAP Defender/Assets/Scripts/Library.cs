using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour {

    public Transform[] Waypoints;
    public Dictionary<string, Transform> Way;
    public Dictionary<string, GameObject> Interactibles;
    public Dictionary<string, GameObject> Attackables;
    public Dictionary<string, int> door;

    void Awake () {
        Way = new Dictionary<string, Transform>();

        Way.Add("anvil", Waypoints[0]);
        Way.Add("mana", Waypoints[1]);
        Way.Add("armory", Waypoints[2]);
        Way.Add("chest", Waypoints[3]);
        Way.Add("treeU", Waypoints[4]);
        Way.Add("treeD", Waypoints[5]);
        Way.Add("mineD", Waypoints[6]);
        Way.Add("mineU", Waypoints[7]);
        Way.Add("doorR", Waypoints[8]); 
        Way.Add("doorU", Waypoints[9]); 
        Way.Add("doorL", Waypoints[10]);
        Way.Add("doorD", Waypoints[11]);
        Way.Add("cross", Waypoints[12]);
        Way.Add("Blacksmith", Waypoints[13]);

        door = new Dictionary<string, int>();

        door.Add("DoorL", 0);
        door.Add("DoorU", 1);
        door.Add("DoorR", 2);
        door.Add("DoorD", 3);
    }
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
