using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_GOAP : Labourer {

    public Library lib;

    public bool[] lastDoorState;
    public int wood;
    public int ore;
    public int backPackSize;


	void Start () {
        //gego = this.GetComponent<GenericGOAP>();
        //StartCoroutine(teste());
        //god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        lastDoorState = new bool[] { true, true, true, true };
        
    }
	
	void Update () {
		
	}

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            if (god.doorStates[i] != lastDoorState[i])
            {
                lastDoorState[i] = god.doorStates[i];
                gego.pf.ChangeDoor(i);
            }
        }
        if(hp <= 0)
        {
            this.gameObject.SetActive(false);
        }

    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("Make tools", true));
        //goal.Add(new KeyValuePair<string, object>("Stay Alive", true));
        //goal.Add(new KeyValuePair<string, object>("Free path", false));
        return goal;
    }
}
