using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RequestCurrentScoreArgs
{
    public int CurrentScore;
}

public class TimeView : MonoBehaviour
{
    [SerializeField] private Text _timeText;
    [SerializeField] private Image _timeCircle;
    [SerializeField] private Color[] _timerColors;

    private const int TIMER_COUNT = 20;
    private float currCountdownValue;
    private bool _timeStop = true;

    public Action<int> TimeFinish;
    public Action<RequestCurrentScoreArgs> RequestCurrentScore;

    public void StartTimer()
    {
        currCountdownValue = TIMER_COUNT;
        _timeStop = false;
    }

    void Update()
    {
        if (!_timeStop)
        {
            currCountdownValue -= Time.deltaTime;

            CheckCurrentCountDownValue();
        }
    }

    internal void OnRoundFinish()
    {
        _timeStop = true;
    }

    private void CheckCurrentCountDownValue()
    {
        if (currCountdownValue >= 1f)
        {
            _timeText.text = currCountdownValue <= 0 ? "0" : ((int)currCountdownValue).ToString();
        }
        else
        {
            _timeText.text = currCountdownValue <= 0 ? "0" : (Math.Round(currCountdownValue, 1)).ToString();
        }

        if (currCountdownValue <= Mathf.Epsilon)
        {
            _timeCircle.fillAmount = 0;
            _timeStop = true;

            RequestCurrentScoreArgs requestCurrentScoreArgs = new RequestCurrentScoreArgs();
            RequestCurrentScore?.Invoke(requestCurrentScoreArgs);

            TimeFinish?.Invoke(requestCurrentScoreArgs.CurrentScore);
        }
        else
        {
            _timeCircle.fillAmount = (currCountdownValue / TIMER_COUNT);

            SetTimerCircleColor();
        }
    }

    private void SetTimerCircleColor()
    {
        if (_timeCircle.fillAmount >= 0.6f)
            _timeCircle.DOColor(_timerColors[0], 1f).SetEase(Ease.Linear);
        else if (_timeCircle.fillAmount >= 0.3f && _timeCircle.fillAmount < 0.6f)
            _timeCircle.DOColor(_timerColors[1], 1f).SetEase(Ease.Linear);
        else
            _timeCircle.DOColor(_timerColors[2], 1f).SetEase(Ease.Linear);
    }

    internal void OnTimeRestart()
    {
        StartTimer();
    }

    public void OnDestinationFailed(int downAmount)
    {
        CountDown(downAmount);
    }

    private void CountDown(int downAmount)
    {
        currCountdownValue -= downAmount;
        CheckCurrentCountDownValue();
    }
}
