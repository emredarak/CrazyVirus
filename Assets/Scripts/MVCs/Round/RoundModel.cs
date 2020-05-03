using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundModel
{
    public Action RoundFinish;

    private int _currentRound;
    public int CurrentRound { get { return _currentRound; } set { _currentRound = value; } }

    internal void OnRoundFinish()
    {
        CurrentRound++;

        CheckMaxRound();

        RoundFinish?.Invoke();
    }

    private void CheckMaxRound()
    {
        if (!PlayerPrefs.HasKey("MaxRound"))
            PlayerPrefs.SetInt("MaxRound", 1);

        int round = PlayerPrefs.GetInt("MaxRound");

        if (CurrentRound > round)
            PlayerPrefs.SetInt("MaxRound", CurrentRound);
    }
}
