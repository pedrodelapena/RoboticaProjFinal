using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_GOAP : Labourer {

    public Library lib;


	void Start () {
        //gego = this.GetComponent<GenericGOAP>();
        //StartCoroutine(teste());
	}
	
	void Update () {
		
	}

    void FixedUpdate()
    {

    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("Damage door", true));
        return goal;
    }
    /*
    IEnumerator teste()
    {
        gego.goTo(lib.Way["treeD"]);
        yield return new WaitUntil(() => gego.inPlace);
        print("cheguei");
    }
    */
}
