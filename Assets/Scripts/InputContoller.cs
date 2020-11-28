using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputContoller : MonoBehaviour
{
    RopeScript ropeScript;
    private LayerMask buttonLayer;
    private LayerMask targetLayer;
    private bool isButtonUp=false;
    private Transform button;
    void Start()
    {
        buttonLayer=LayerMask.GetMask("End");
        targetLayer=LayerMask.GetMask("Target");
        /*GameObject pipe=GameObject.Find("Pipe");
        ropeScript=pipe.GetComponent<>*/
        /*print(buttonLayer.value);
        print(targetLayer.value);*/
    }
    // Update is called once per frame
    void Update()
    {
        userInput();
    }

    void userInput(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Input.GetTouch (0).position
        RaycastHit hitInfo;
        RaycastHit hitInfo2;
        if (Input.GetMouseButtonDown(0))
        {
            if (!isButtonUp)
            {
                if (Physics.Raycast(ray, out hitInfo, buttonLayer))
                {
                    print("1");
                    print(hitInfo.transform.name + "1");
                    print(hitInfo.transform.position);
                    button = hitInfo.transform;
                    button.position = Vector3.MoveTowards(button.position, button.position + Vector3.up, 1);
                    isButtonUp = true;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hitInfo2, targetLayer))
                {
                    if (isButtonUp && button != null)
                    {
                        MenuManager.numberOfMove--;
                        print("3");
                        print(hitInfo2.transform.name + "3");
                        print(hitInfo2.transform.position);
                        /*button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position + new Vector3(0, 1, 0), 1);
                        button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position, 1);*/
                        button.position = hitInfo2.transform.position;
                        isButtonUp = false;
                    }
                }
            }
        }

    }
}
