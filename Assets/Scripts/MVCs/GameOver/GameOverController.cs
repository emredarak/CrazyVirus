using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController
{
    private GameOverModel _model;
    private GameOverView _view;

    public Action<int> TimeFinish;

    public GameOverController(GameOverModel model, GameOverView view)
    {
        _model = model;
        _view = view;

        RegisterController();
    }

    private void RegisterController()
    {
        TimeFinish += _view.OnTimeFinish;
    }

    private void UnRegisterController()
    {
        TimeFinish -= _view.OnTimeFinish;
    }

    internal void Initialize()
    {
        _view.Initialize();
    }

    internal void OnTimeFinish(int currentScore)
    {
        TimeFinish?.Invoke(currentScore);
    }
}
