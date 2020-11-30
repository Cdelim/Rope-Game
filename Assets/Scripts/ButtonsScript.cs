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
            if (button!=null && Vector3.Distance(button.transform.position, position) < 0.5f)
                return true;
        }
        return false;
    }
    public  void deleteButton(GameObject button)
    {

        List<GameObject> tempList = new List<GameObject>(buttons);
        tempList.Remove(button.gameObject);
        buttons = tempList.ToArray();
    }
}
