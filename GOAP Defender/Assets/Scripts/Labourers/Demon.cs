using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Demon : Labourer
{
    /**
	 * Our only goal will ever be to make tools.
	 * The ForgeTooldAction will be able to fulfill this goal.
	 */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("killBS", true));
        goal.Add(new KeyValuePair<string, object>("destroyDoor", true));
        goal.Add(new KeyValuePair<string, object>("takeCross", true));
        return goal;
    }
}

