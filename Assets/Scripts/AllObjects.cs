using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllObject", order = 1)]
public class AllObjects : ScriptableObject
{
    [SerializeField] private List<Sprite> _objectSprite;
    public List<Sprite> ObjectSprite => _objectSprite;
}
