using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{

    #region Singleton Pattern

    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion //Singleton Pattern

    async Task Start()
    {
        DontDestroyOnLoad(gameObject);

        await StartGame();
    }

    private async Task StartGame()
    {
        PlayStateModel playStateModel = PlayStateFactory.Instance.CreatePlayStateModel();
        GameObject playStateViewGameObject = await PlayStateFactory.Instance.CreatePlayStateView();
        PlayStateView playStateView = playStateViewGameObject.GetComponent<PlayStateView>();
        PlayStateController playStateController = PlayStateFactory.Instance.CreatePlayStateController(playStateModel, playStateView);
        await playStateController.Initialize();
    }
}
