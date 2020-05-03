using System.Threading.Tasks;
using UnityEngine;

public class TimeFactory
{
    //Thread-safe & lazy singleton implementation
    #region Singleton

    private static readonly TimeFactory _instance = new TimeFactory();
    public static TimeFactory Instance => _instance;

    static TimeFactory() { }

    private TimeFactory() { }

    #endregion //Singleton

    private const string TIME_VIEW_NAME = "Time";

    public TimeModel CreateTimeModel()
    {
        return new TimeModel();
    }

    public async Task<TimeView> CreateTimeView(Transform parent)
    {
        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(TIME_VIEW_NAME, parent);
        return gameObject.GetComponent<TimeView>();
    }

    public TimeController CreateTimeController(TimeModel timeModel, TimeView timeView)
    {
        return new TimeController(timeModel, timeView);
    }
}
