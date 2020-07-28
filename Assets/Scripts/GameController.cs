using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    //public GUIText scoreText;
    public Text scoreTextOnCanvas;
    public Text gameOver;
    public Text restart;

    private bool gameoverBool;
    private bool restartBool;

    private int score;


    IEnumerator SpawnWaves()
    {
        // first wave wait some seconds
        yield return new WaitForSeconds(startWait);


        // while loop keeps to generat waves 
        while (true) {

            // for loop generat one wave of asteroid of hazardCount 
            for (int i = 0; i < hazardCount; i++) {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];


                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            // time between 2 waves
            yield return new WaitForSeconds(waveWait);

            if (gameoverBool)
            {
                restart.text = "Press 'R' to restart";
                restartBool = true;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine( SpawnWaves());
        GetComponent<AudioSource>().Play();
        scoreTextOnCanvas.text = "Score: 0";
        gameOver.text = "";
        restart.text = "";
        gameoverBool = false;
        restartBool = false;
    }

    private void Update()
    {
      if (restartBool)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
     
       
    }

    void UpdateScore()
    {
        //scoreText.text = "Score: " + score;
        scoreTextOnCanvas.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOver.text = "Game Over!";
        gameoverBool = true;
    }
}
