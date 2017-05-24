using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericGOAP : MonoBehaviour {

    PathFind pf;
    public Library lib;
    public bool inPlace;

    void Awake () {
        pf = this.GetComponent<PathFind>();
        lib = GameObject.FindGameObjectWithTag("Library").GetComponent<Library>();
	}
	
    void Start()
    {

    }

	void Update () {
        inPlace = (transform.position.x == pf.target.position.x && transform.position.z == pf.target.position.z);
    }

    public void goTo(Transform Waypoint)
    {
        pf.target = Waypoint;
        //print("Going to " + Waypoint.position);
    }

    public void setSpeed(float speed)
    {
        pf.changeSpeed(speed);
    }

    public void goToClosestIteractible()
    {
        Transform[] interactibles = new Transform[lib.Way.Count];
        lib.Way.Values.CopyTo(interactibles, 0);
        float dist = 99999;
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        foreach (Transform t in interactibles)
        {
            NavMesh.CalculatePath(transform.position, t.position, agent.areaMask, path);
            agent.SetPath(path);

            if (agent.remainingDistance < dist)
            {
                dist = agent.remainingDistance;
                pf.target = t;
            }
        }
    }

    public Dictionary<string,float> getObjectsDist(){ //to-do: make this sh%$ work!
        Dictionary<string, float> dic = new Dictionary<string, float>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        NavMeshPath oldPath = agent.path;
        foreach (string key in lib.Way.Keys)
        {
            Transform trans = lib.Way[key];
            NavMesh.CalculatePath(transform.position, trans.position, agent.areaMask, path);
            agent.SetPath(path);
            dic.Add(key, agent.remainingDistance);
            print(" key:" + key + " - " + agent.remainingDistance);
            print(agent.path.corners.Length);
        }
        agent.path = oldPath;
        return dic;
    }
}
