using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class CreateAddressablesLoader
{

    public static async Task<GameObject> CreateGameObject(string gameObjectName, Transform parent = null)
    {
        var location = await Addressables.LoadResourceLocationsAsync(gameObjectName).Task;
        GameObject gameObject = await Addressables.InstantiateAsync(location[0],parent).Task as GameObject;

        return gameObject;
    }

    public static async Task<T> CreateAsset<T>(string assetName) where T : Object
    {
        var location = await Addressables.LoadResourceLocationsAsync(assetName).Task;
        return await Addressables.LoadAssetAsync<T>(location[0]).Task as T;
    }

    public static async Task CreateGameObjects<GameObject>(string labelName, List<GameObject> createdGameObjects) where GameObject : Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(labelName).Task;
        foreach (var location in locations)
            createdGameObjects.Add(await Addressables.InstantiateAsync(location).Task as GameObject);
    }

    public static async Task CreateAssets<T>(string labelName, List<T> createdAssets) where T : Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(labelName).Task;
        foreach (var location in locations)
            createdAssets.Add(await Addressables.LoadAssetAsync<T>(location).Task as T);
    }

}
