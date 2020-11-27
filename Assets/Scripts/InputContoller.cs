using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputContoller : MonoBehaviour
{
    RopeScript ropeScript;
    private int buttonLayer;
    private int targetLayer;
    private bool isButtonUp=false;
    private Transform button;
    private float smooth=0.2f;
    void Start()
    {
        buttonLayer=LayerMask.GetMask("End");
        targetLayer=LayerMask.GetMask("Target");
        /*GameObject pipe=GameObject.Find("Pipe");
        ropeScript=pipe.GetComponent<>*/
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
            if (Physics.Raycast(ray, out hitInfo,buttonLayer))
            {
                if (!isButtonUp)
                {
                    print("1");
                    print(hitInfo.transform.name);
                    button = hitInfo.transform;
                    button.position = Vector3.MoveTowards(button.position, button.position + Vector3.up, smooth);
                    //hitInfo.transform.Translate(Vector3.up * Time.deltaTime);
                    isButtonUp = true;
                }
                else
                {
                    print("2");
                    button.position = Vector3.MoveTowards(button.position, button.position + Vector3.down, smooth);
                    //hitInfo.transform.Translate(Vector3.down * Time.deltaTime);
                    isButtonUp = false;
                }
            }
            if (Physics.Raycast(ray, out hitInfo, targetLayer))
            {
                if (isButtonUp && button != null)
                {
                    print("3");
                    button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position + new Vector3(0, 1, 0), smooth);
                    button.position = Vector3.MoveTowards(button.position, hitInfo.transform.position, smooth);
                }
            }
        }

    }
}
