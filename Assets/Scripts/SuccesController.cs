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
        ignoreMasks = (LayerMask.GetMask("End","Target","Default"));
        //ignoreMasks += ropeScript.ropeMask;
    }

     void Update()
     {
        //Touch touch = Input.GetTouch(0);
        //switch (touch.phase)
        //{
          //  case TouchPhase.Ended:
          //if(Input.touchCount<=0)
                isSuccesful();
         //       break;
       // }
     }

    void isSuccesful()
    {
        //if (Input.GetMouseButtonUp(0)){// Input.GetTouch (0)
        isKnot = false;
        for (int i = 1; i < ropeScript.joints.Length; i++) {
            Ray ray = new Ray(ropeScript.joints[i].transform.position + (Vector3.down * 5), Vector3.up);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10, ~(ignoreMasks))) //|| Physics.Raycast(ray2, out hit2, 3,~ignoreMasks))
            {
                if (!hit.transform.name.StartsWith(LayerMask.LayerToName(ropeScript.joints[i].layer)))
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.green);
                    isKnot = true;
                    break;
                }
                /*print(hit.transform.name);
                Debug.DrawLine(ray.origin, hit.point , Color.green);
                print("123");*/
            }
            else {

                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.blue);
            }


        }
        //print(isKnot);
        if (!isKnot && Time.time>5.0f && Input.GetMouseButtonUp(0)) {
            print("asd");
            //ropeScript.DestroyRope();
            //GameController.numberOfRope--;
            //Destroy(this);
        }
        //}

    }
}
