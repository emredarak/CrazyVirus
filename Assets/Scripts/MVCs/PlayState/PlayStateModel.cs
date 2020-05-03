using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayStateModel
{
    private const string OBJECT_NAME = "Object";
    private const string OBJECT_SPRITES = "ObjectsSprite";
    private const string NEXT_ROUND_PARTICLE = "NextRoundParticle";

    private GameObject _object;
    private AllObjects _objectSprites;
    private ParticleSystem _nextRoundParticle;

    public AllObjects ObjectSprites { set { _objectSprites = value; } get { return _objectSprites; } }
    public GameObject Object { set { _object = value; } get { return _object; } }
    public ParticleSystem NextRoundParticle { set { _nextRoundParticle = value; } get { return _nextRoundParticle; } }

    public async Task LoadObjectSprites()
    {
        ObjectSprites = await CreateAddressablesLoader.CreateAsset<AllObjects>(OBJECT_SPRITES);
        Object = await CreateAddressablesLoader.CreateAsset<GameObject>(OBJECT_NAME);

        GameObject gameObject = await CreateAddressablesLoader.CreateGameObject(NEXT_ROUND_PARTICLE);
        NextRoundParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        NextRoundParticle.Stop();
    }
}
