using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationObjectController
{
    private DestinationObjectModel _model;
    private DestinationObjectView _view;

    public DestinationObjectController(DestinationObjectModel model, DestinationObjectView view)
    {
        _model = model;
        _view = view;
    }

    public void SetDestinationSprite(Sprite destinationSprite, GameObject objectPrefab, out int destinationPlace)
    {
        _view.SetDestinationSprite(destinationSprite, objectPrefab, out destinationPlace);
    }

    public void OnRoundFinish()
    {
        _view.DestroyDestinationObject();
    }
}
