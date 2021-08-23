﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //[SerializeField] private int _size = 5;
    [SerializeField] private GameObject[] _playerIcon; 
    [SerializeField] private GameObject[] _playerSelected;
    [SerializeField] private Text _timer;
    [SerializeField] private GameObject _endGame;
    [Space]
    [SerializeField] private CellComponent[] _cells;
    [SerializeField] private StickListener[] _verticalSticks;
    [SerializeField] private StickListener[] _horizontalSticks;

    private GameSession _session;
    private int _count;
    private bool _playerMovesAgain;
    private float _time;
    private int _countPoint;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _count = 1;

        for (int i = 0; i < _session.QuantityOfPlayers; i++)
        {
            _playerIcon[i].SetActive(true);
        }

        _countPoint = _cells.Length;

        _playerSelected[0].SetActive(true);
        _timer.text = "00:00";
        _time = _session.Timer;
        StartCoroutine(StartTimer());
    }

    public Color WhoseMoveNow()
    {
        switch (_count)
        {
            case 1:
                return _session.Player1;
            case 2:
                return _session.Player2;
            case 3:
                return _session.Player3;
            case 4:
                return _session.Player4;
            default:
                return _session.Player1;
        }
    }

    public void MoveIsMade()
    {
        

        _playerMovesAgain = false;
        CheckingTheCell();
        
        if (!_playerMovesAgain)
        {
            StopAllCoroutines();
            StartCoroutine(StartTimer());
            
            _playerMovesAgain = false;
            
            _playerSelected[_count - 1].SetActive(false);
            ChangeMove();
            _playerSelected[_count - 1].SetActive(true);
        }
        
    }

    private IEnumerator StartTimer()
    {
        var delay = _time;
        while (delay >= 0)
        {
            _timer.text = $"{00}:{delay}";
            delay--;
            yield return new WaitForSeconds(1f);
        }
        MoveIsMade();
    }

    public void ChangeMove()
    {
        if (_count < _session.QuantityOfPlayers)
            _count++;
        else
            _count = 1;
    }

    public void CheckingTheCell()
    {        
        for (int i = 0; i < _cells.Length; i++)
        {
            if (i >= 0 && i < 5)
            {
                if (_horizontalSticks[i].Standing && _horizontalSticks[i + 5].Standing && _verticalSticks[i].Standing && _verticalSticks[i + 1].Standing)
                    ChangingTheCell(i);
            }
            else if (i >= 5 && i < 10)
            {
                if (_horizontalSticks[i].Standing && _horizontalSticks[i + 5].Standing && _verticalSticks[i + 1].Standing && _verticalSticks[i + 2].Standing)
                    ChangingTheCell(i);
            }
            else if (i >= 10 && i < 15)
            {
                if (_horizontalSticks[i].Standing && _horizontalSticks[i + 5].Standing && _verticalSticks[i + 2].Standing && _verticalSticks[i + 3].Standing)
                    ChangingTheCell(i);
            }
            else if (i >= 15 && i < 20)
            {
                if (_horizontalSticks[i].Standing && _horizontalSticks[i + 5].Standing && _verticalSticks[i + 3].Standing && _verticalSticks[i + 4].Standing)
                    ChangingTheCell(i);
            }
            else
            {
                if (_horizontalSticks[i].Standing && _horizontalSticks[i + 5].Standing && _verticalSticks[i + 4].Standing && _verticalSticks[i + 5].Standing)
                    ChangingTheCell(i);
            }
        }
    }

    private void ChangingTheCell(int i)
    {
        if (!_cells[i].ChangedTheColor)
        {
            _playerMovesAgain = true;
            _cells[i].ChangeColor(WhoseMoveNow());
            _session.SetPoint(_count - 1);

            _countPoint--;
            if (_countPoint == 0)
                EndGame();
        }
    }

    private void EndGame()
    {
        StopAllCoroutines();
        _endGame.SetActive(true);
    }
}