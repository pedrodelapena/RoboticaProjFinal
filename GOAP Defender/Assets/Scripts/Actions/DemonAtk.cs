using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAtk : GoapAction {
    private bool atacked = false;
    private Labourer soldier; // who we aim at
    private GenericGOAP gego;

    private float startTime = 0;
    public float workDuration = 0; // seconds
    public float distFactor = 100;
    float oldCost;

    void Awake()
    {
        gego = gameObject.GetComponent<GenericGOAP>();
        oldCost = cost;
    }

    public DemonAtk()
    {
        addEffect("Damage door", true);
        cost = 5;
    }


    public override void reset()
    {
        atacked = false;
        soldier = null;
        startTime = 0;
        cost = oldCost;
        
    }

    public override bool isDone()
    {
        return atacked;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest chopping block that we can chop our wood at

        Dictionary<string, float> targetsDist = gego.getObjectsDist();
        float dist = 999f;
        foreach (string name in targetsDist.Keys)
        {
            GameObject temp = gego.lib.Way[name].gameObject;
            if (targetsDist[name] < dist && (temp.tag.Equals("NPC") || temp.tag.Equals("Door")))
            {
                dist = targetsDist[name];
                soldier = temp.GetComponentInParent<Labourer>();
                print(targetsDist[name] + " - " + dist);
            }
        }

        cost += (distFactor * dist) / 500;

        target = soldier.gameObject;

        if(soldier.hp <= 0 || dist == 999f)
        {
            return false;
        }

        return target != null;
    }

    public override bool perform(GameObject agent)
    {
        print("Bati");

            startTime = Time.time;
        if (startTime == 0)
        {
            workDuration = 1 / gameObject.GetComponent<Labourer>().atkSpeed;
            soldier.decreaseHp(1);
        }
        if (Time.time - startTime > workDuration)
        {
            atacked = true;
        }
        return true;
    }
}
