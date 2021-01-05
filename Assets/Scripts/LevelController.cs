using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    //Public Variables
    public static LevelController instance;
    public Text pointsText;
    public GameObject gamePanel;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public float gameSpeed = 2;
    public int obstaclesAmount = 6;
    public ObjectPool pickupPool;
    public float damageTime = 0.01f;
    public Color easyColor, mediumColor, hardColor;
    public float obstaclesDistance = 13;
    public Vector2 xLimit;
    public float multiplier = 1;
    public float cicleTime = 5;
    public AudioClip clickSound;
    public bool gameOver = true;

    //Private Variables
    private int points;
    private Transform player;

    //Courotine
    IEnumerator Start()
    {
        instance = this;
        player = FindObjectOfType<Player>().transform;

        while (gameOver)
        {
            yield return null;
        }

        SpawnPickups();
        InvokeRepeating("IncreaseDifficulty", cicleTime, cicleTime);

    }

    //Responsible for the Start of the game
    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        gamePanel.SetActive(true);
        startPanel.SetActive(false);
        gameOver = false;
    }

    //responsible for the game over
    public void GameOver()
    {
        gameOver = true;
        gameSpeed = 0;

        gameOverPanel.SetActive(true);
    }

    //Reloads the scene when player lose
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //This method is responsible for the player points
    public void Score(int amount)
    {
        points += amount;

        pointsText.text = points.ToString();
    }

    //Method that increase the dificulty by changing multiplier and obstacleAmount
    void IncreaseDifficulty()
    {
        obstaclesAmount += 2;

        multiplier *= 1.1f;
    }

    //This function invoke the method SpawnPickups
    void SpawnPickups()
    {
        pickupPool.GetObject().transform.position = new Vector2(Random.Range(xLimit.x, xLimit.y), player.position.y + 15);

        Invoke("SpawnPickups", Random.Range(1f, 3f));

    }

}
