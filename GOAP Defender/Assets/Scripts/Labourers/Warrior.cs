﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warrior : Labourer
{
    /**
	 * Our only goal will ever be to make tools.
	 * The ForgeTooldAction will be able to fulfill this goal.
	 */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("protectBS", true));
        goal.Add(new KeyValuePair<string, object>("killDemons", true));
        return goal;
    }
}
