﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private Text[] _players;
    private GameSession _session;

    private void OnEnable()
    {
        _session = GameSession.Instance;
        int count;
        if (_session.AIMode)
            count = _session.QuantityOfAI;
        else
            count = _session.QuantityOfPlayers;

        var names = _session.GetName();
        
        int max = 0;
        int indexMax = 0;

        for (int i = 0; i < count; i++)
        {
            _players[i].gameObject.SetActive(true);
            _players[i].text = $"{names[i]} : {_session.PlayersPoints[i]}";
            if (_session.PlayersPoints[i] > max)
            {
                max = _session.PlayersPoints[i];
                indexMax = i;
            }                
        }

        _players[indexMax].color = _session.Player1;
    }

    public void OnExit()
    {
        _session.ClearPoints();
        _session.DestroySession();
        SceneManager.LoadScene("MainMenu");
    }
}
