using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : GoapAction {


    private bool crafted = false;
    private GameObject anvil; // who we aim at
    private GenericGOAP gego;

    private float startTime = 0;
    public float workDuration; // seconds
    public float oldCost;


    void Awake()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        gego = gameObject.GetComponent<GenericGOAP>();
        oldCost = cost;
        anvil = gego.lib.Way["anvil"].gameObject;
    }

    public Craft()
    {
        addEffect("Make tools", true);
        addPrecondition("Has ore", true);
        addPrecondition("Has wood", true);
    }


    public override void reset()
    {
        crafted = false;
        startTime = 0;
        cost = oldCost;

    }

    public override bool isDone()
    {
        return crafted;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest chopping block that we can chop our wood at

        Dictionary<string, float> targetsDist = gego.getObjectsDist();
        float dist = targetsDist["anvil"];

        cost = oldCost + (god.distFactor * dist) / 300;

        if (anvil == null)
            return false;
        target = anvil;

        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            print(gameObject.name + " tentou craftar ");
            if (god.wood <= 3 || god.ore <= 3)
            {  
                return false;
            }
            else
            {
                god.wood -= 3;
                god.ore -= 3;
            }
        }

        if (Time.time - startTime > workDuration)
        {
            god.tools["sword"] += 1;
            crafted = true;
        }

        return true;
    }
}
