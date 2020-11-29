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

    void isSuccesful()
    {
        //Ray ray = new Ray(ropeScript.joints[i].transform.position-(Vector3.down*5), Vector3.up);
        //RaycastHit hit;
        //if (Input.GetMouseButtonUp(0)){// Input.GetTouch (0)
            isKnot = false;
            for (int i = 1; i < ropeScript.joints.Length; i++) {
                Ray ray = new Ray(ropeScript.joints[i].transform.position + (Vector3.down * 5), Vector3.up);
                /*Ray ray = new Ray(ropeScript.joints[i].transform.position , Vector3.down);
                Ray ray2 = new Ray(ropeScript.joints[i].transform.position, Vector3.up);*/
                /*Ray ray = new Ray(ropeScript.joints[i].transform.position, new Vector3(0,1,-1));
                Ray ray2 = new Ray(ropeScript.joints[i].transform.position, new Vector3(0, -1, 1));*/

                RaycastHit hit;
                RaycastHit hit2;
            if (Physics.Raycast(ray, out hit, 20, ~(ignoreMasks))) //|| Physics.Raycast(ray2, out hit2, 3,~ignoreMasks))
            {
                //print(hit.transform.name);
                if (hit.transform.name.StartsWith(ropeScript.ropeMask.ToString()))
                {
                  
                    return;
                }
                isKnot = true;
                Debug.DrawLine(ray.origin, hit.point , Color.green);
                //if (Physics.Raycast(ray2, out hit2, 10, ignoreMasks))
                //Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * 3, Color.blue);
                print("123");
            }
            else {

                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.blue);
            }
                /*if (Physics.Raycast(ray2, out hit2, 5, ~ignoreMasks))
                {

                    print(hit2.transform.name);
                    isKnot = true;
                }*/
                /*else {
                    Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * 3, Color.black);
                }*/


            }
            if (!isKnot) {
                print("asd");
                ropeScript.DestroyRope();
                GameController.numberOfRope--;
                Destroy(this);
            }
        //}

    }
}
