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
        if (Input.GetMouseButtonDown(0))
        {
            if (!isButtonUp)
            {
                if (Physics.Raycast(ray, out hitInfo,20 ,buttonLayer))
                {
                    button = hitInfo.transform;
                    button.position = Vector3.MoveTowards(button.position, button.position + Vector3.up, 1);
                    isButtonUp = true;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hitInfo, 20 ,targetLayer))
                {
                    if (isButtonUp && button != null)
                    {
                        if (Vector3.Distance(button.position, hitInfo.transform.position) > 1)
                        {
                            MenuManager.numberOfMove--;
                        }
                        /*button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position + new Vector3(0, 1, 0), 1);
                        button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position, 1);*/
                        button.position = hitInfo.transform.position;
                        isButtonUp = false;
                    }
                }
            }
        }

    }
}
