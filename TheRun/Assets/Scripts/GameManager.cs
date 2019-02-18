﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int MAX_SCORE = 999999;

    public GameObject textGameOver;
    public GameObject textClear;
    public GameObject buttons;
    public GameObject textScoreNumber;

    public enum GAME_MODE
    {
        PLAY,
        CLEAR,
    }

    public GAME_MODE gameMode = GAME_MODE.PLAY;

    private int score = 0;
    private int displayScore = 0;

    void Start()
    {
        RefreshScore();
    }

    void Update()
    {
        if (score > displayScore)
        {
            displayScore += 10;

            if (displayScore > score)
            {
                displayScore = score;
            }

            RefreshScore();
        }
    }

    public void GameClear()
    {
        gameMode = GAME_MODE.CLEAR;
        textClear.SetActive(true);
        buttons.SetActive(false);
    }

    public void GameOver()
    {
        textGameOver.SetActive(true);
        buttons.SetActive(false);
    }

    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }
    }

    private void RefreshScore()
    {
        textScoreNumber.GetComponent<Text>().text = displayScore.ToString();
    }
}
