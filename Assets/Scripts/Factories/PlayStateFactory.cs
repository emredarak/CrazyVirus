using System.Threading.Tasks;
using UnityEngine;

public class PlayStateFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly PlayStateFactory _instance = new PlayStateFactory();
    public static PlayStateFactory Instance => _instance;

    static PlayStateFactory() { }

    private PlayStateFactory() { }

    #endregion //Singleton

    private const string PLAY_STATE_VIEW_NAME = "PlayStateView";

    public PlayStateModel CreatePlayStateModel()
    {
        return new PlayStateModel();
    }

    public async Task<GameObject> CreatePlayStateView()
    {
        return await CreateAddressablesLoader.CreateGameObject(PLAY_STATE_VIEW_NAME); ;
    }

    public PlayStateController CreatePlayStateController(PlayStateModel model, PlayStateView view)
    {
        return new PlayStateController(model, view);
    }
}
