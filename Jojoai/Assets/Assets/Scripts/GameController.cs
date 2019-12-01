using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool _admitPlayers = true;
    public float _admitPlayersTimer;
    private GameObject[] players;
    private bool godSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _admitPlayersTimer -= Time.deltaTime;

        if (_admitPlayersTimer < 0f)
        {
            _admitPlayers = false;
        }
      
        if (!godSelected && !_admitPlayers)
        {
            players = GameObject.FindGameObjectsWithTag("User");
            players[Random.Range(0, players.Length)].GetComponent<Ascend>().Ascension();
            godSelected = true;          
        }


        // Update players score:
        for (int i = 0; i < players.Length; ++i)
        {
            //players[i].transform.Find("moa").GetComponent<Player>().score += Time.deltaTime;
        }
    }
}
