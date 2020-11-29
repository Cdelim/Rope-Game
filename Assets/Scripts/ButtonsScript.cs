using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    [HideInInspector]
    GameObject[] buttons;
    void Awake()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }


    public bool isHereFill(Vector3 position) {
        foreach (GameObject button in buttons) {
            if (Vector3.Distance(button.transform.position, position) < 0.5f)
                return true;
        }
        return false;
    }
}
