using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLog : GoapAction {


    private bool got = false;
    private Transform tree; // who we aim at
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
        blackSmithDude = gameObject.GetComponent<BS_GOAP>();
    }

    public GatherLog()
    {
        addEffect("Has wood", true);
        addPrecondition("Has wood", false);
    }


    public override void reset()
    {
        got = false;
        startTime = 0;
        cost = oldCost;

    }

    public override bool isDone()
    {
        return got;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest chopping block that we can chop our wood at

        Dictionary<string, float> targetsDist = gego.getObjectsDist();
        float dist = targetsDist["treeU"] < targetsDist["treeD"] ?  targetsDist["treeU"] : targetsDist["treeD"];
        tree = targetsDist["treeU"] < targetsDist["treeD"] ? gego.lib.Way["treeU"] : gego.lib.Way["treeD"];

        cost = oldCost + (god.distFactor * dist) / 300 + god.wood;

        if (tree == null)
            return false;
        target = tree.gameObject;

        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            print(gameObject.name + " foi cata madeira ");
            if (blackSmithDude.wood >= blackSmithDude.backPackSize)
            {
                got = true;
                return true;
            }
        }

        if (Time.time - startTime > workDuration)
        {
            blackSmithDude.wood += 1;
            return false;
        }

        return true; ;
    }
}
