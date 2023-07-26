using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Road_Script : MonoBehaviour
{
    [SerializeField]
    public PathCreator _Path;

    PathCreator GetRoadPath()
    {
        return _Path;
    }

}
