using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Script : MonoBehaviour
{
    public enum Road_Type
    {
        straight,
        Tsplit,
        cross,
        turn,
        end
    }

    public Road_Type roadType = Road_Type.straight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
