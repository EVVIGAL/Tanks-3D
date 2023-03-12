using UnityEngine;

public class DisableBulletOfScreen : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;

    private void OnBecameInvisible()
    {
        _projectile.gameObject.SetActive(false);
    }
}