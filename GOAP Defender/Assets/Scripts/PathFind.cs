using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFind : MonoBehaviour {


    NavMeshAgent me;
    public LineRenderer line;
    public Transform target;
    public Library lib;


	// Use this for initialization
	void Start () {
        me = gameObject.GetComponent<NavMeshAgent>();
        //line = this.GetComponent<LineRenderer>();
        ChangeDoor(lib.door["DoorD"], true);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        me.SetDestination(target.position);
        //if(target != lastTarget)  -- use this if code is too heavy

        plotLine();
    }

    void ChangeDoor(int door, bool open)
    {
        int value = (int)Mathf.Pow(2, door + 3);
        me.areaMask += open? value:-value;
        //area mask works by converting the int value to binary and using it as bolleans for each layer on the NavMeshAgent AreaMaks componnent.
    }

    void plotLine()
    {
        Vector3[] Pos = new Vector3[me.path.corners.Length + 1];
        Pos[0] = transform.position;
        int count = 1;
        foreach(Vector3 i in me.path.corners)
        {
            Pos[count] = i;
            count++;
        }
        line.SetVertexCount(Pos.Length);
        line.SetPositions(Pos);
    }

    public void changeSpeed(float speed)
    {
        me.speed = speed;
    }

}
