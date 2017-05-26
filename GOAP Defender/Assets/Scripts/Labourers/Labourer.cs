using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * A general labourer class.
 * You should subclass this for specific Labourer classes and implement
 * the createGoalState() method that will populate the goal for the GOAP
 * planner.
 */
public abstract class Labourer : MonoBehaviour, IGoap
{
    public GenericGOAP gego;
    public float atkSpeed;
    public int hp;
    public static God god;  

	public float moveSpeed = 1;

    void Awake()
    {
        gego = GetComponent<GenericGOAP>();
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        
    }
	void Start ()
	{

	}
	

	void Update ()
	{

    }

	/**
	 * Key-Value data that will feed the GOAP actions and system while planning.
	 */
	public HashSet<KeyValuePair<string,object>> getWorldState () {
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();
        worldData.Add(new KeyValuePair<string, object>("Free path", (!god.doorStates[0] || !god.doorStates[1] || !god.doorStates[2] || !god.doorStates[3])));
        worldData.Add(new KeyValuePair<string, object>("Wood in box", god.wood > 0));
        worldData.Add(new KeyValuePair<string, object>("Ore in box", god.ore > 0));
        if (gameObject.GetComponent<BS_GOAP>())
        {
            BS_GOAP bs = gameObject.GetComponent<BS_GOAP>();
            worldData.Add(new KeyValuePair<string, object>("Has ore", bs.ore >= bs.backPackSize));
            worldData.Add(new KeyValuePair<string, object>("Has wood", bs.wood >= bs.backPackSize));
        }

        //print((!god.doorStates[0] || !god.doorStates[1] || !god.doorStates[2] || !god.doorStates[3]) + "!!!!");

        /*
		worldData.Add(new KeyValuePair<string, object>("hasOre", (backpack.numOre > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasLogs", (backpack.numLogs > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasFirewood", (backpack.numFirewood > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasTool", (backpack.tool != null) ));
        */
        return worldData;
	}

	/**
	 * Implement in subclasses
	 */
	public abstract HashSet<KeyValuePair<string,object>> createGoalState ();


	public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal)
	{
        // Not handling this here since we are making sure our goals will always succeed.
        // But normally you want to make sure the world state has changed before running
        // the same goal again, or else it will just fail.

	}

	public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
	{
		// Yay we found a plan for our goal
		Debug.Log ("<color=green>Plan found</color> "+GoapAgent.prettyPrint(actions));
	}

	public void actionsFinished ()
	{
		// Everything is done, we completed our actions for this gool. Hooray!

		Debug.Log ("<color=blue>Actions completed</color>");
	}

	public void planAborted (GoapAction aborter)
	{
		// An action bailed out of the plan. State has been reset to plan again.
		// Take note of what happened and make sure if you run the same goal again
		// that it can succeed.
		Debug.Log ("<color=red>Plan Aborted</color> "+GoapAgent.prettyPrint(aborter));
        gameObject.GetComponent<GoapAgent>().createIdleState();
        //print("FAIOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
    }

	public bool moveAgent(GoapAction nextAction) {
        // move towards the NextAction's target
        gameObject.GetComponent<GoapAgent>().createIdleState();
        gego.goTo(nextAction.target.transform);
        gego.setSpeed(moveSpeed);
		
		if (gameObject.transform.position.x == nextAction.target.transform.position.x && gameObject.transform.position.z == nextAction.target.transform.position.z) {
			// we are at the target location, we are done
			nextAction.setInRange(true);
            print("<Color=blue>Got in Range</color>");
			return true;
		} else
			return false;
	}

    public void increaseHp(int delta)
    {
        hp += delta;
    }
    public void decreaseHp(int delta)
    {
        hp -= delta;
    }
}

