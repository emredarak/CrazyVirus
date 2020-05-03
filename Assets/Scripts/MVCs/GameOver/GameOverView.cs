using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Text _scoreValue;
    [SerializeField] private Text _highScoreValue;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private void Start()
    {
        _restartButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();

        _restartButton.onClick.AddListener(RestartButton);
        _mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    internal void OnTimeFinish(int currentScore)
    {
        gameObject.SetActive(true);

        int maxRoundValue;

        if (!PlayerPrefs.HasKey("MaxRound"))
            PlayerPrefs.SetInt("MaxRound", 1);

        maxRoundValue = PlayerPrefs.GetInt("MaxRound");

        _scoreValue.text = currentScore.ToString();
        _highScoreValue.text = maxRoundValue.ToString();
    }

    internal void Initialize()
    {
        gameObject.SetActive(false);
    }

    private void MainMenuButton()
    {
        
    }

    private void RestartButton()
    {
        
    }
}
