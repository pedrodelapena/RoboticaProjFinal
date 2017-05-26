﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWoodBox : GoapAction {


    private bool picked = false;
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
        blackSmithDude = gameObject.GetComponent<BS_GOAP>();
    }

    public GetWoodBox()
    {
        addEffect("Has wood", true);
        addPrecondition("Wood in box",true);
    }

    public override void reset()
    {
        picked = false;
        startTime = 0;
        cost = oldCost;

    }

    public override bool isDone()
    {
        return picked;
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
        chest = gego.lib.Way["chest"];

        cost = oldCost + ((god.distFactor * dist) / 100) - (god.wood) / 10;

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
            print(gameObject.name + " pegou stuffs on the caixa");
            if (blackSmithDude.wood >= 10 || god.wood <= 0  )
            {
                picked = blackSmithDude.wood > 2;
                return false;
            }
        }

        if (Time.time - startTime > workDuration)
        {
            blackSmithDude.wood += 1;
            god.wood -= 1;
            return false;
        }

        return true; ;
    }
}
