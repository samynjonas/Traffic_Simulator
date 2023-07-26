using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower_Demo : MonoBehaviour
{
    [SerializeField]
    public PathCreator _Path;

    public float _Speed = 5;

    public float _DistanceTravelled;

    // Update is called once per frame
    void Update()
    {
        _DistanceTravelled += _Speed * Time.deltaTime;
        transform.position = _Path.path.GetPointAtDistance(_DistanceTravelled);
        transform.rotation = _Path.path.GetRotationAtDistance(_DistanceTravelled);
    }
}
