using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using DG.Tweening;

public class HorizontalView : MonoBehaviour
{
    [SerializeField] private List<Transform> _objectsTransform;

    private List<Vector3> _objectsTransformPositions = new List<Vector3>();
    private List<Transform> _objectsTransformChangeable = new List<Transform>();

    public Sprite DestinationSprite { get; private set; }
    public int DestinationPlace;
    private const int DOWN_AMOUNT = 5;
    private bool _moveActive = true;

    public Action DestinationSuccess;
    public Action<int> DestinationFailed;


    public void GenerateObject(AllObjects objectsSprite, GameObject objectPrefab)
    {
        int index = UnityEngine.Random.Range(0, _objectsTransform.Count);

        for (int i = 0; i < _objectsTransform.Count; i++)
        {
            GameObject objectInstantiate = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);

            objectInstantiate.GetComponent<Image>().sprite = objectsSprite.ObjectSprite[UnityEngine.Random.Range(0, 15)];
            objectInstantiate.transform.SetParent(_objectsTransform[i], false);

            _objectsTransformPositions.Add(_objectsTransform[i].localPosition);

            if (index == i)
            {
                DestinationSprite = objectInstantiate.GetComponent<Image>().sprite;
            }

            _objectsTransformChangeable.Add(_objectsTransform[i]);
        }
    }

    public void NextRoundPrepare(AllObjects objectsSprite)
    {
        int index = UnityEngine.Random.Range(0, _objectsTransform.Count);
        Color32 color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < _objectsTransform.Count; i++)
        {
            Image image = _objectsTransform[i].GetComponentInChildren<Image>();
            image.color = color;

            image.sprite = objectsSprite.ObjectSprite[UnityEngine.Random.Range(0, 15)];

            if (index == i)
            {
                DestinationSprite = image.sprite;
            }
        }
    }

    internal void OnRight()
    {
        if (_moveActive)
        {
            _moveActive = false;

            Transform temp = _objectsTransformChangeable[_objectsTransformChangeable.Count - 1];
            _objectsTransformChangeable.RemoveAt(_objectsTransformChangeable.Count - 1);
            _objectsTransformChangeable.Insert(0, temp);

            foreach (var objectsTransform in _objectsTransform)
            {
                int findIndex = -10;

                for (int i = 0; i < _objectsTransformPositions.Count; i++)
                {
                    if (objectsTransform.localPosition == _objectsTransformPositions[i])
                    {
                        findIndex = i;
                    }
                }

                if (findIndex == _objectsTransformPositions.Count - 1)
                {
                    objectsTransform.localPosition = _objectsTransformPositions[0];
                }
                else
                {
                    if (findIndex != -10)
                        objectsTransform.DOLocalMove(_objectsTransformPositions[findIndex + 1], .2f).OnComplete(MoveFinished);    //localPosition = _objectsTransformPositions[findIndex + 1];
                }
            }
        }
    }

    internal void OnLeft()
    {
        if (_moveActive)
        {
            _moveActive = false;

            Transform temp = _objectsTransformChangeable[0];
            _objectsTransformChangeable.RemoveAt(0);
            _objectsTransformChangeable.Add(temp);

            foreach (var objectsTransform in _objectsTransform)
            {
                int findIndex = -10;

                for (int i = 0; i < _objectsTransformPositions.Count; i++)
                {
                    if (objectsTransform.localPosition == _objectsTransformPositions[i])
                    {
                        findIndex = i;
                    }
                }

                if (findIndex == 0)
                {
                    objectsTransform.localPosition = _objectsTransformPositions[_objectsTransformPositions.Count - 1];
                }
                else
                {
                    if (findIndex != -10)
                        objectsTransform.DOLocalMove(_objectsTransformPositions[findIndex - 1], .2f).OnComplete(MoveFinished);     //localPosition = _objectsTransformPositions[findIndex - 1];
                }
            }
        }
    }

    internal void OnDown()
    {
        if (CheckDestination())
        {
            OpacityDown();
            DestinationSuccess?.Invoke();
            //Destroy(this.gameObject);
        }
        else
        {
            DestinationFailed?.Invoke(DOWN_AMOUNT);
        }
    }

    private void MoveFinished()
    {
        _moveActive = true;
    }

    private bool CheckDestination()
    {
        Sprite objectSprite = _objectsTransformChangeable[DestinationPlace].GetComponentInChildren<Image>().sprite;

        if (objectSprite == DestinationSprite)
            return true;
        else
            return false;
    }

    public void OpacityDown()
    {
        Color32 color = new Color32(255, 255, 255, 35);

        foreach (var objectTransform in _objectsTransform)
        {
            objectTransform.GetComponentInChildren<Image>().color = color;
        }
    }
}
