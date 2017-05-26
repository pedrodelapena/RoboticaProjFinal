using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAtk : GoapAction
{
    private bool atacked = false;
    private Labourer soldier; // who we aim at
    private GenericGOAP gego;

    private float startTime = 0;
    public float workDuration = 0; // seconds
    public float oldCost;

    void Awake()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        gego = gameObject.GetComponent<GenericGOAP>();
        oldCost = cost;
    }

    public DemonAtk()
    {


        addEffect("Free path", true);

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
        float dist = 800f;
        foreach (string name in targetsDist.Keys)
        {
            GameObject temp = gego.lib.Way[name].gameObject;
            if (targetsDist[name] < dist && (temp.tag.Equals("NPC") || temp.tag.Equals("Door")) && temp.gameObject.activeInHierarchy)
            {
                dist = targetsDist[name];
                soldier = temp.GetComponentInParent<Labourer>();

            }
        }

        cost = oldCost + (god.distFactor * dist) / 300;

        if (soldier == null)
            return false;
        target = soldier.transform.FindChild("waypoint").gameObject;

        if (soldier.hp <= 0 || dist > 900f)
            return false;

        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            workDuration = 1 / gameObject.GetComponent<Labourer>().atkSpeed;
            soldier.decreaseHp(1);
            print(gameObject.name + " Bateu no " + soldier.name);
            if (soldier.hp <= 0)
            {
                if (soldier.gameObject.tag.Equals("Door"))
                    god.breakDoor(soldier.gameObject.GetComponent<DoorLab>().DoorId);
                    atacked = true;
                return true;
            }
            //print(Effects.GetObjectData);
        }

        if (Time.time - startTime > workDuration)
            return false;
        return true;
    }
}