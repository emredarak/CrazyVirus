using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    private TimeModel _model;
    private TimeView _view;

    private Action<int> DestinationFailed;
    private Action TimeRestart;
    public Action RoundFinish;
    public Action<int> TimeFinish;
    public Action<RequestCurrentScoreArgs> RequestCurrentScore;

    public TimeController(TimeModel model, TimeView view)
    {
        _model = model;
        _view = view;

        RegisterController();

        _view.StartTimer();
    }

    public void RegisterController()
    {
        _view.TimeFinish += OnTimeFinish;
        _view.RequestCurrentScore += OnRequestCurrentScore;

        DestinationFailed += _view.OnDestinationFailed;
        TimeRestart += _view.OnTimeRestart;
        RoundFinish += _view.OnRoundFinish;
    }

    public void UnRegisterController()
    {
        _view.TimeFinish -= OnTimeFinish;
        _view.RequestCurrentScore -= OnRequestCurrentScore;

        DestinationFailed -= _view.OnDestinationFailed;
        TimeRestart -= _view.OnTimeRestart;
        RoundFinish -= _view.OnRoundFinish;
    }

    public void OnRequestCurrentScore(RequestCurrentScoreArgs requestCurrentScoreArgs)
    {
        RequestCurrentScore?.Invoke(requestCurrentScoreArgs);
    }

    private void OnTimeFinish(int currentScore)
    {
        TimeFinish?.Invoke(currentScore);
    }

    internal void OnDestinationFailed(int downAmount)
    {
        DestinationFailed?.Invoke(downAmount);
    }

    internal void OnTimeRestart()
    {
        TimeRestart?.Invoke();
    }

    internal void OnRoundFinish()
    {
        RoundFinish?.Invoke();
    }
}