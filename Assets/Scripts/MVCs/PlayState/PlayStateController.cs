using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayStateController
{
    private PlayStateModel _model;
    private PlayStateView _view;

    private List<HorizontalController> CurrentHorizontalControllers = new List<HorizontalController>();
    private DestinationObjectController _destinationObjectController;
    private int _currentHorizontalControlIndex = 0;

    private Action RoundFinish;
    private Action TimeRestart;
    private Action<int> TimeFinish;

    private TimeController _timeController;
    private RoundController _roundController;
    private GameOverController _gameOverController;

    public PlayStateController(PlayStateModel model, PlayStateView view)
    {
        _model = model;
        _view = view;
    }

    public async Task Initialize()
    {
        await _model.LoadObjectSprites();

        await CreateSubMVCsAsync();

        RegisterController();
    }

    private async Task CreateSubMVCsAsync()
    {
        await CreateTimeMVCAsync();
        await CreateRoundMVCAsync();
        await CreateGameOverMVCAsync();

        for (int i = 0; i < 6; i++)
        {
            await CreateHorizontalMVCAsync(_view.VerticalGameObject[i]);
        }

        await CreateDestinationMVCAsync();
    }

    private async Task CreateGameOverMVCAsync()
    {
        GameOverModel gameOverModel = GameOverFactory.Instance.CreateGameOverModel();
        GameOverView gameOverView = await GameOverFactory.Instance.CreateGameOverView(_view.GameAreaView);
        _gameOverController = GameOverFactory.Instance.CreateGameOverController(gameOverModel, gameOverView);
        _gameOverController.Initialize();

        TimeFinish += _gameOverController.OnTimeFinish;
    }

    private async Task CreateTimeMVCAsync()
    {
        TimeModel timeModel = TimeFactory.Instance.CreateTimeModel();
        TimeView timeView = await TimeFactory.Instance.CreateTimeView(_view.GameAreaView);
        _timeController = TimeFactory.Instance.CreateTimeController(timeModel, timeView);

        _timeController.TimeFinish += OnTimeFinish;
        TimeRestart += _timeController.OnTimeRestart;
        RoundFinish += _timeController.OnRoundFinish;
    }

    private async Task CreateRoundMVCAsync()
    {
        RoundModel roundModel = RoundFactory.Instance.CreateRoundModel();
        RoundView roundView = await RoundFactory.Instance.CreateRoundView(_view.GameAreaView);
        _roundController = RoundFactory.Instance.CreateRoundController(roundModel, roundView);

        _roundController.Initialize();

        _timeController.RequestCurrentScore += _roundController.OnRequestCurrentScore;
        RoundFinish += _roundController.OnRoundFinish;
    }

    private async Task CreateHorizontalMVCAsync(Transform parent)
    {
        HorizontalModel horizontalModel = HorizontalFactory.Instance.CreateHorizontaModel();
        HorizontalView horizontalView = await HorizontalFactory.Instance.CreateHorizontalView(parent);
        HorizontalController horizontalController = HorizontalFactory.Instance.CreateHorizontaController(horizontalModel, horizontalView);

        horizontalController.Initialize(_model.ObjectSprites, _model.Object);
        horizontalController.DestinationSuccess += OnDestinationSuccess;
        horizontalController.DestinationFailed += _timeController.OnDestinationFailed;
        CurrentHorizontalControllers.Add(horizontalController);
        _currentHorizontalControlIndex = CurrentHorizontalControllers.Count - 1;
    }

    private async Task CreateDestinationMVCAsync()
    {
        DestinationObjectModel destinationObjectModel = DestinationObjectFactory.Instance.CreateDestinationObjectModel();
        DestinationObjectView destinationObjectView = await DestinationObjectFactory.Instance.CreateDestinationObjectView(_view.DestinationGameObject);
        _destinationObjectController = DestinationObjectFactory.Instance.CreateDestinationObjectController(destinationObjectModel, destinationObjectView);

        RoundFinish += _destinationObjectController.OnRoundFinish;
        SetDestinationSprite();
    }

    public void RegisterController()
    {
        _view.HorizontalRight += OnRight;
        _view.HorizontalLeft += OnLeft;
        _view.HorizontalDown += OnDown;
    }

    public void UnRegisterController()
    {
        _view.HorizontalRight -= OnRight;
        _view.HorizontalLeft -= OnLeft;
        _view.HorizontalDown -= OnDown;
    }

    private void OnDestinationSuccess()
    {
        _currentHorizontalControlIndex--;

        if (_currentHorizontalControlIndex < 0)
        {
            RoundFinish?.Invoke();

            PlayNextRoundParticle();
        }
        else
        {
            SetDestinationSprite();
        }
    }

    private void PlayNextRoundParticle()
    {
        float duration = _model.NextRoundParticle.main.duration;
        _model.NextRoundParticle.gameObject.SetActive(true);
        _model.NextRoundParticle.Play();

        _view.StartCoroutine(NextRoundPrepare(duration));
    }

    private IEnumerator NextRoundPrepare(float duration)
    {
        yield return new WaitForSeconds(duration);

        _model.NextRoundParticle.Pause();
        _model.NextRoundParticle.gameObject.SetActive(false);

        _currentHorizontalControlIndex = CurrentHorizontalControllers.Count - 1;

        foreach (var currentHorizontalController in CurrentHorizontalControllers)
        {
            currentHorizontalController.NextRoundPrepare(_model.ObjectSprites);
        }

        SetDestinationSprite();

        TimeRestart?.Invoke();
    }

    private void SetDestinationSprite()
    {
        HorizontalView horizontalView = CurrentHorizontalControllers[_currentHorizontalControlIndex].View;

        _destinationObjectController.SetDestinationSprite(horizontalView.DestinationSprite, _model.Object, out horizontalView.DestinationPlace);
    }

    private void OnTimeFinish(int currentScore)
    {
        TimeFinish?.Invoke(currentScore);

        _currentHorizontalControlIndex = -1;
    }

    private void OnDown()
    {
        if (_currentHorizontalControlIndex >= 0)
            CurrentHorizontalControllers[_currentHorizontalControlIndex].OnDown();
    }

    private void OnLeft()
    {
        if (_currentHorizontalControlIndex >= 0)
            CurrentHorizontalControllers[_currentHorizontalControlIndex].OnLeft();
    }

    private void OnRight()
    {
        if (_currentHorizontalControlIndex >= 0)
            CurrentHorizontalControllers[_currentHorizontalControlIndex].OnRight();
    }
}
