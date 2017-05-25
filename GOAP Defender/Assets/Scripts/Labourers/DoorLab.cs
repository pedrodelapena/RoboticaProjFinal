using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLab : Labourer {

    public int DoorId;

    void Awake()
    {
        hp = 10;
        //god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
    }

    void Update()
    {
        if (hp <= 0)
        {
            god.breakDoor(DoorId);
        }
        else
        {
            god.fixDoor(DoorId);
        }
    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("Break", false));
        return goal;
    }
}
