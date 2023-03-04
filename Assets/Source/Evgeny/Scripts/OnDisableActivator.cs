using UnityEngine;

public class OnDisableActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    private void OnDisable()
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(true);
    }
}