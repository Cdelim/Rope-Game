using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (RopeScript))]
public class SuccesController : MonoBehaviour
{
    private RopeScript ropeScript;
    //private bool isKnot = true;
    //private LayerMask ignoreMasks;
    private static GameObject[] ropes;
    void  Awake()
    {
        ropeScript = GetComponent<RopeScript>();
        //ignoreMasks = (LayerMask.GetMask("End","Target","Default"));
        ropes = GameObject.FindGameObjectsWithTag("Rope");
        //ignoreMasks += ropeScript.ropeMask;
    }

     void Update()
     {
        if (Input.GetMouseButtonDown(0) ) {
            if (isSuccess())
            {
                List<GameObject> tempList = new List<GameObject>(ropes);
                tempList.Remove(this.gameObject);
                ropes = tempList.ToArray();


                ropeScript.DestroyRope();
                GameController.numberOfRope--;
                Destroy(this);
            }
        }
     }

    /*void isSuccesful()
    {
        if (Input.GetMouseButtonUp(0)){// Input.GetTouch (0)
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
            }
            else {

                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.blue);
            }


        }
            if (!isKnot && Time.time>5.0f && Input.GetMouseButtonUp(0)) {
                ropeScript.DestroyRope();
                GameController.numberOfRope--;
                Destroy(this);
            }
        }

    }*/
    bool isSuccess() {
        float startPosX = transform.position.x;
        float endPosX = ropeScript.target.position.x;
        foreach (GameObject rope in ropes) {
            if (rope.name == this.name)
            {
                continue;
            }
            else {
                if ((startPosX <rope.transform.position.x && endPosX>rope.transform.GetChild(rope.transform.childCount - 1).position.x)
                    || ( startPosX > rope.transform.position.x && endPosX < rope.transform.GetChild(rope.transform.childCount - 1).position.x)) {
                    return false;
                }
                else {
                    for (int i = 1; i < rope.transform.childCount - 1; i++) {
                        if ((startPosX < rope.transform.position.x && transform.GetChild(i).position.x > rope.transform.GetChild(i).position.x)
                            || startPosX > rope.transform.position.x && transform.GetChild(i).position.x < rope.transform.GetChild(i).position.x)
                            return false;
                    }
                }
            }
        }
        return true;
    }
}
