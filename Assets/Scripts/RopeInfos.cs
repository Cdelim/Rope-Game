using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class RopeInfos : MonoBehaviour
{
    public static GameObject[] ropes;


    void Start()
    {
        ropes = GameObject.FindGameObjectsWithTag("Rope");
    }

}
