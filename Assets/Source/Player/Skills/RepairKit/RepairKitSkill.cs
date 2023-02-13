using UnityEngine;

public class RepairKitSkill : Skill
{
    [SerializeField] private RepairKit _repairKitTemplate;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private Camera _camera;

    protected override void OnUse()
    {
        Vector3 centerOfScreen = _camera.transform.position;
        centerOfScreen.z = 0f;
        centerOfScreen.y = _spawnHeight;
        Instantiate(_repairKitTemplate, centerOfScreen, Quaternion.identity);
    }
}