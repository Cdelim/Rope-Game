using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int numberOfMove = 7;
    [SerializeField] Text movesText;
    [SerializeField] GameObject gameOver;
    private void Awake()
    {
        updateText();
        FindObjectOfType<GameController>().onGameOver += OnGameOver;
    }
    private void Update()
    {
        updateText();
    }
    private void updateText() {
        movesText.text = "Moves " + numberOfMove.ToString();
    }
    private void OnGameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0;

    }
    public void OnRestart() {
        gameOver.SetActive(false);
        numberOfMove = 10;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
