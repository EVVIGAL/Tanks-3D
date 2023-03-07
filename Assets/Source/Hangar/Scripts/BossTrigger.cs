using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private BossArtBlow _art;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth target))
        {
            _art.gameObject.SetActive(true);
            _art.SetTarget(target.transform);
        }
    }
}