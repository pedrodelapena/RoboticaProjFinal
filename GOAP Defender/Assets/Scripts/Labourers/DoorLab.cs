using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLab : Labourer {

    public God god;
    public int DoorId;

    void Awake()
    {
        hp = 10;
    }

    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            god.doorStates[DoorId] = false;
        }
        else
        {
            gameObject.SetActive(true);
            god.doorStates[DoorId] = true;
        }
    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("Break", false));
        return goal;
    }
}
