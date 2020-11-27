using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (RopeScript))]
public class SuccesController : MonoBehaviour
{
    private RopeScript ropeScript;
    void  Awake()
    {
        ropeScript = GetComponent<RopeScript>();
    }

    // Update is called once per frame
     void Update()
    {
        //base.Update();
        isSuccesful();
    }

    void isSuccesful(){
        Vector3 direction = ropeScript.target.position- transform.position ;
          Ray ray=new Ray(transform.position,direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            //ropeScript.DestroyRope();
        }
        else {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }

    }
}
