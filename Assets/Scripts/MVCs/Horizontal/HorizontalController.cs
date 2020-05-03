using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HorizontalController
{
    private HorizontalModel _model;
    private HorizontalView _view;

    public HorizontalView View { get { return _view; } }

    private Action HorizontalLeft;
    private Action HorizontalRight;
    private Action HorizontalDown;
    public Action<int> ErrorMatch;
    public Action DestinationSuccess;
    public Action<int> DestinationFailed;

    public HorizontalController(HorizontalModel model, HorizontalView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize(AllObjects objectsSprite, GameObject objectPrefab)
    {
        RegisterController();
        _view.GenerateObject(objectsSprite, objectPrefab);
    }

    public void RegisterController()
    {
        HorizontalLeft += _view.OnLeft;
        HorizontalRight += _view.OnRight;
        HorizontalDown += _view.OnDown;

        _view.DestinationSuccess += OnDestinationSuccess;
        _view.DestinationFailed += OnDestinationFailed;
    }

    public void UnRegisterController()
    {
        HorizontalLeft -= _view.OnLeft;
        HorizontalRight -= _view.OnRight;
        HorizontalDown -= _view.OnDown;

        _view.DestinationSuccess -= OnDestinationSuccess;
        _view.DestinationFailed -= OnDestinationFailed;
    }

    private void OnDestinationFailed(int downAmount)
    {
        DestinationFailed?.Invoke(downAmount);
    }

    private void OnDestinationSuccess()
    {
        DestinationSuccess?.Invoke();
    }

    public void OnDown()
    {
        HorizontalDown?.Invoke();
    }

    public void OnLeft()
    {
        HorizontalLeft?.Invoke();
    }

    public void OnRight()
    {
        HorizontalRight?.Invoke();
    }

    public void NextRoundPrepare(AllObjects objectsSprite)
    {
        _view.NextRoundPrepare(objectsSprite);
    }
}
