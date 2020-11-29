using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event System.Action onGameOver;
    //public event System.Action onLevelFinish;

    public static int numberOfRope;
    void Start()
    {
        numberOfRope = (GameObject.Find("Pipe").transform.childCount-1)/2; //Minus 1 for panel and divided by 2 because there are start and end.
    }

    void Update()
    {
        isGameOver();
        isLevelFinish();
    }

    void isGameOver() {
        if (MenuManager.numberOfMove <= -1) {
            if (onGameOver != null) onGameOver();
        }
    }
    void isLevelFinish() {
        if (numberOfRope <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
