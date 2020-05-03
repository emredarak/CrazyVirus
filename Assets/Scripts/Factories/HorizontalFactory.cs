using System;
using System.Threading.Tasks;
using UnityEngine;

public class HorizontalFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly HorizontalFactory _instance = new HorizontalFactory();
    public static HorizontalFactory Instance => _instance;

    static HorizontalFactory() { }

    private HorizontalFactory() { }

    #endregion //Singleton

    private const string HORIZONTAL_VIEW_NAME = "HorizontalView";

    public HorizontalModel CreateHorizontaModel()
    {
        return new HorizontalModel();
    }

    public async Task<HorizontalView> CreateHorizontalView(Transform parent)
    {
        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(HORIZONTAL_VIEW_NAME, parent);
        return gameObject.GetComponent<HorizontalView>();
    }

    public HorizontalController CreateHorizontaController(HorizontalModel horizontalModel, HorizontalView horizontalView)
    {
        return new HorizontalController(horizontalModel, horizontalView);
    }
}

