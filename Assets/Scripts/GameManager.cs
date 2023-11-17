using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;

public class GameManager : NetworkBehaviour
{

    public NetworkManager networkManager;
    public List<Player> playerList;

    public bool gameIsRunning;

    public float timeRemaining = 10;

    public bool timerIsRunning = false;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        networkManager = this.GetComponent<NetworkManager>();

    }
    void Update()
    {
        if(networkManager != null){
            Debug.Log("Network Manager:" + networkManager);
        }
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Debug.Log("Time Remaining: " + timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
              //  NetworkManager.Destroy();
            }
        }
    }
    
}
