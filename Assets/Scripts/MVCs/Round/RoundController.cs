using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController
{
    private RoundModel _model;
    private RoundView _view;

    private Action RoundFinish;

    public RoundController(RoundModel model, RoundView view)
    {
        _model = model;
        _view = view;

        _model.CurrentRound = 1;
        _view.Model = _model;
    }

    internal void Initialize()
    {
        RegisterController();
    }

    public void RegisterController()
    {
        RoundFinish += _model.OnRoundFinish;
        _model.RoundFinish += _view.OnRoundFinish;
    }

    public void UnRegisterController()
    {
        RoundFinish -= _model.OnRoundFinish;
        _model.RoundFinish -= _view.OnRoundFinish;
    }

    internal void OnRoundFinish()
    {
        RoundFinish?.Invoke();
    }

    internal void OnRequestCurrentScore(RequestCurrentScoreArgs requestCurrentScoreArgs)
    {
        requestCurrentScoreArgs.CurrentScore = _model.CurrentRound;
    }
}
