using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gc = gameControllerObject.GetComponent<GameController>();
        }

        if(gc== null)
        {
            Debug.Log("Cannot find 'GameController' script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        if (other.tag == "Boundary" || other.tag == "Asteroid" || other.tag == "Enemy")
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        

        if (other.tag == "Player") { 
        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gc.gameOver.text = "Game Over";
            gc.GameOver();

        }

        gc.AddScore(scoreValue);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
