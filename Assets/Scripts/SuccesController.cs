using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (RopeScript))]
public class SuccesController : MonoBehaviour
{
    private RopeScript ropeScript;
    private bool isKnot = true;
    private LayerMask ignoreMasks;
    void  Awake()
    {
        ropeScript = GetComponent<RopeScript>();
        ignoreMasks = (LayerMask.GetMask("End","Target","Default",ropeScript.ropeMask.ToString()));
        //ignoreMasks += ropeScript.ropeMask;
    }

     void Update()
     {
        //Touch touch = Input.GetTouch(0);
        //switch (touch.phase)
        //{
          //  case TouchPhase.Ended:
                isSuccesful();
         //       break;
       // }
     }

    void isSuccesful(){
        if (Input.GetMouseButtonUp(0)){// Input.GetTouch (0)
            isKnot = true;
            for (int i = 1; i < ropeScript.joints.Length; i++) {
                Ray ray = new Ray(ropeScript.joints[i].transform.position, Vector3.down);
                Ray ray2 = new Ray(ropeScript.joints[i].transform.position, Vector3.up);
                /*Ray ray = new Ray(ropeScript.joints[i].transform.position, new Vector3(0,1,-1));
                Ray ray2 = new Ray(ropeScript.joints[i].transform.position, new Vector3(0, -1, 1));*/

                RaycastHit hit;
                RaycastHit hit2;
                if (Physics.Raycast(ray, out hit, 5, ~ignoreMasks) || Physics.Raycast(ray2, out hit2, 5, ~ignoreMasks)) //|| Physics.Raycast(ray2, out hit2, 3,~ignoreMasks))
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 3, Color.green);
                    //if (Physics.Raycast(ray2, out hit2, 10, ignoreMasks))
                    Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * 3, Color.blue);
                    isKnot = false;
                }


            }
            if (isKnot) {
                //ropeScript.DestroyRope();
                //GameController.numberOfRope--;
                //Destroy(this);
            }
        }

    }
}
