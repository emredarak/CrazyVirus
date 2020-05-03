using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameOverFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly GameOverFactory _instance = new GameOverFactory();
    public static GameOverFactory Instance => _instance;

    static GameOverFactory() { }

    private GameOverFactory() { }

    #endregion //Singleton

    private const string GAME_OVER_VIEW_NAME = "GameOverView";

    public GameOverModel CreateGameOverModel()
    {
        return new GameOverModel();
    }

    public async Task<GameOverView> CreateGameOverView(Transform parent)
    {
        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(GAME_OVER_VIEW_NAME, parent);
        return gameObject.GetComponent<GameOverView>();
    }

    public GameOverController CreateGameOverController(GameOverModel gameOverModel, GameOverView gameOverView)
    {
        return new GameOverController(gameOverModel, gameOverView);
    }
}
