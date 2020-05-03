using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationObjectView : MonoBehaviour
{
    [SerializeField] private List<Transform> _destinationTransforms;
    GameObject _objectInstantiate;

    public List<Transform> DestinationTransforms { get { return _destinationTransforms; } }

    public void SetDestinationSprite(Sprite destinationSprite, GameObject objectPrefab, out int destinationPlace)
    {
        DestroyDestinationObject();

        int place = UnityEngine.Random.Range(1, _destinationTransforms.Count - 1);
        destinationPlace = place;

        _objectInstantiate = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        _objectInstantiate.GetComponent<Image>().sprite = destinationSprite;
        _objectInstantiate.transform.SetParent(DestinationTransforms[place], false);
    }

    public void DestroyDestinationObject()
    {
        if (_objectInstantiate)
            Destroy(_objectInstantiate);
    }
}
