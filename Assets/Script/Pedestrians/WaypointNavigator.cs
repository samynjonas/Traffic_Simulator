using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    //This controls what the next waypoint will be

    CharacterNavigationController controller;

    public Waypoint currentWaypoint;

    public bool _RandomizedDirection = true;
    int direction = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_RandomizedDirection)
        {
            direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        }

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(controller._ReachedDestination)
        {
            bool shouldBranch = false;

            //If there is a branch on the waypoint
            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0) 
            { 
                //Decide if the npc should branch or not depending on the branchRatio
                shouldBranch = Random.Range(0, 1f) <= currentWaypoint.branchRatio ? true : false;
            }

            if(shouldBranch) 
            {
                //If it should branch - the next waypoint is a random branched waypoint
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
            }
            else
            {
                //Check which direction the npc is going
                if (direction == 0)
                {
                    if(currentWaypoint.nextWaypoint != null) 
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if(currentWaypoint.previousWaypoint != null) 
                    { 
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }
            }            

            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
