using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class RequestObjectPrefab
{
    public GameObject ObjectPrefab;

    public RequestObjectPrefab(GameObject objectPrefab)
    {
        ObjectPrefab = objectPrefab;
    }
}

public class PlayStateView : MonoBehaviour
{
    [SerializeField] private List<Transform> _verticalGameObject;
    [SerializeField] private Transform _destinationGameObject;
    [SerializeField] private Transform _gameAreaView;

    public Transform DestinationGameObject { get { return _destinationGameObject; } }
    public Transform GameAreaView { get { return _gameAreaView; } }

    public List<Transform> VerticalGameObject { get { return _verticalGameObject; } }

    public Action HorizontalLeft;
    public Action HorizontalRight;
    public Action HorizontalDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HorizontalLeft.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HorizontalRight.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HorizontalDown.Invoke();
        }
    }

    private void OnDestroy()
    {
        HorizontalLeft = null;
        HorizontalRight = null;
        HorizontalDown = null;
    }
}
