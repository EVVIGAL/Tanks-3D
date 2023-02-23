using UnityEngine;

public class ArtBlowSkill : Skill
{
    [SerializeField] private uint _damage;
    [SerializeField] private Bomb _artBlowTemplate;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _xOffset;

    protected override void OnUse()
    {
        Vector3 centerOfScreen = _camera.transform.position;
        centerOfScreen.z = 0f;
        centerOfScreen.y = _spawnHeight;
        centerOfScreen.x += _xOffset;
        Projectile newBomb = Instantiate(_artBlowTemplate);
        newBomb.transform.SetPositionAndRotation(centerOfScreen, Quaternion.LookRotation(Vector3.right));
        newBomb.Init(_damage);
    }
}