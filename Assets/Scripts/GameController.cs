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
        numberOfRope = GameObject.Find("Pipe").transform.childCount;
    }

    void Update()
    {
        isGameOver();
        isLevelFinish();
    }

    void isGameOver() {
        if (MenuManager.numberOfMove <= 0) {
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
