using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameObject levelComplete;
    bool triggered = false;
    string player;
     float timeForRefresh = 3f;
    private void Update()
    {
        if(triggered)
        {
            if(Time.time > timeForRefresh)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        levelComplete.GetComponentInChildren<WhoWon>().winnerText.text = "Player " + other.gameObject.tag + " won";
        Instantiate(levelComplete);
        timeForRefresh = Time.time + 3f;
    }
}
