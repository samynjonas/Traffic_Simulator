using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSearcher : MonoBehaviour
{
    [SerializeField] private Waypoint _GoalWaypoint = null;

    [SerializeField] private Waypoint _StartWaypoint = null;

    //private Waypoint _CurrentWaypoint = null;

    private List<Waypoint> _CheckedWaypoints = new List<Waypoint>();
    private List<Waypoint> _Path = new List<Waypoint>();

    private void Start()
    {
        SearchToGoal(_StartWaypoint);

        Debug.Log("Path");
        foreach (var waypoint in _Path)
        {
            Debug.Log("-" + waypoint.name);
        }
    }

    public bool SearchToGoal(Waypoint current)
    {
        if(current == null)
        {
            return false;
        }

        foreach(var waypointcheck in _CheckedWaypoints)
        {
            if(waypointcheck == current)
            {
                Debug.Log("Already checked");
                return false;
            }
        }
        _CheckedWaypoints.Add(current);
        _Path.Add(current);

        Debug.Log("Search " + current.name);
        if (current == _GoalWaypoint)
        {
            Debug.Log("FOUND");
            return true;
        }

        Debug.Log("Search next");
        if(SearchToGoal(current.nextWaypoint))
        {
            Debug.Log("FOUND");
            return true;
        }

        Debug.Log("Search previous");
        if (SearchToGoal(current.previousWaypoint))
        {
            Debug.Log("FOUND");
            return true;
        }

        foreach (var branchedCheckpoint in current.branches)
        {
            Debug.Log("Search Branch");
            if (SearchToGoal(branchedCheckpoint))
            {
                Debug.Log("FOUND");
                return true;
            }
        }

        _Path.Remove(current);
        Debug.Log("Not Found");
        return false;
    }
}
