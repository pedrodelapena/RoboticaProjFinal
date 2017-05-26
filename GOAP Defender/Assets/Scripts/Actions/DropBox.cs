using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : GoapAction {

    private bool dropped = false;
    private Transform chest; // who we aim at
    private GenericGOAP gego;
    private BS_GOAP blackSmithDude;

    private float startTime = 0;
    public float workDuration; // seconds
    public float oldCost;



    void Awake()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        gego = gameObject.GetComponent<GenericGOAP>();
        oldCost = cost;
    }

    public DropBox()
    {
        addEffect("Ore in box", true);
        addEffect("Wood in box", true);
    }

    public override void reset()
    {
        dropped = false;
        startTime = 0;
        cost = oldCost;

    }

    public override bool isDone()
    {
        return dropped;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest chopping block that we can chop our wood at

        Dictionary<string, float> targetsDist = gego.getObjectsDist();
        float dist = targetsDist["chest"];

        cost = oldCost + ((god.distFactor * dist) / 100 ) - (40 - god.ore - god.wood) / 40;
        
        if (chest == null)
            return false;
        target = chest.gameObject;

        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            print(gameObject.name + " dropou stuffs on the caixa");
            if (god.wood >= 20 && god.ore >= 20)
            {
                dropped = true;
                return true;
            }
        }

        if (Time.time - startTime > workDuration)
        {
            blackSmithDude.wood -= 1;
            god.wood += 1;
            blackSmithDude.ore -= 1;
            god.ore += 1;
            return false;
        }

        return true; ;
    }
}
