using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCross : GoapAction {
    private bool Won = false;
    private GameObject cross; // we are near the sweet, sweet power of god.
    private GenericGOAP gego;
    private float lastDist;

    private float startTime = 0;
    public float workDuration = 3; // the cross is heavy for balancing porpouses
    public float oldCost;

    void Awake()
    {
        gego = gameObject.GetComponent<GenericGOAP>();
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        oldCost = cost;
        cross = GameObject.FindGameObjectWithTag("Cross");
    }
    void Start()
    {
    }
    public GetCross()
    {
        addEffect("Get cross", true);
        addPrecondition("Free path", true);
    }


    public override void reset()
    {
        Won = false;
        startTime = 0;
        cost = oldCost;

    }

    public override bool isDone()
    {
        return Won;
    }

    public override bool requiresInRange()
    {
        return true; 
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {

        Dictionary<string, float> targetsDist = gego.getObjectsDist();
        float dist = 999f;
        foreach (string name in targetsDist.Keys)
        {
            GameObject temp = gego.lib.Way[name].gameObject;
            if (targetsDist[name] < dist && temp.tag.Equals("Cross") && temp.gameObject.activeInHierarchy)
            {
                dist = targetsDist[name];
                cross = temp.gameObject;
            }
        }


        cost = oldCost + (god.distFactor * dist) / 300;
        print(cost + "meeeeeeeeeeeee");

        if (cross == null)
            return false;
        target = cross;
        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            print(gameObject.name + " is taking the " + cross.name);
        }

        if (Time.time - startTime > workDuration)
        {
            Won = true;
            god.loseGame();
        }
        return true;
    }
}
