using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoundFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly RoundFactory _instance = new RoundFactory();
    public static RoundFactory Instance => _instance;

    static RoundFactory() { }

    private RoundFactory() { }

    #endregion //Singleton

    private const string ROUND_VIEW_NAME = "RoundView";

    public RoundModel CreateRoundModel()
    {
        return new RoundModel();
    }

    public async Task<RoundView> CreateRoundView(Transform parent)
    {
        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(ROUND_VIEW_NAME, parent);
        return gameObject.GetComponent<RoundView>();
    }

    public RoundController CreateRoundController(RoundModel roundModel, RoundView roundView)
    {
        return new RoundController(roundModel, roundView);
    }
}
