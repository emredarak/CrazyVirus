using System;
using System.Threading.Tasks;
using UnityEngine;

public class DestinationObjectFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly DestinationObjectFactory _instance = new DestinationObjectFactory();
    public static DestinationObjectFactory Instance => _instance;

    static DestinationObjectFactory() { }

    private DestinationObjectFactory() { }

    #endregion //Singleton

    private const string DESTINATION_VIEW_NAME = "DestinationObject";

    public DestinationObjectModel CreateDestinationObjectModel()
    {
        return new DestinationObjectModel();
    }

    public async Task<DestinationObjectView> CreateDestinationObjectView(Transform parent)
    {
        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(DESTINATION_VIEW_NAME, parent);
        return gameObject.GetComponent<DestinationObjectView>();
    }

    public DestinationObjectController CreateDestinationObjectController(DestinationObjectModel destinationObjectModel, DestinationObjectView destinationObjectView)
    {
        return new DestinationObjectController(destinationObjectModel, destinationObjectView);
    }
}

