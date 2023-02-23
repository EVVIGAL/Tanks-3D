using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<TObject> : MonoBehaviour where TObject : Component
{
    [SerializeField] private TObject _template;
    [SerializeField] private int _poolCapacity = 1;

    protected List<TObject> Pool { get; private set; } = new();

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
            Increase();
    }

    public TObject Create(Transform parent = null, Vector3 position = new(), Quaternion rotation = new())
    {
        TObject result = Pool.Find(@object => @object.gameObject.activeSelf == false);

        if (result == null)
        {
            Increase();
            result = Pool[Pool.Count - 1];
        }

        result.transform.parent = parent;
        result.transform.position = position;
        result.transform.rotation = rotation;
        result.gameObject.SetActive(true);
        OnCreate(result);
        return result;
    }

    protected virtual void OnCreate(TObject @object) { }

    private void Increase()
    {
        TObject newObject = Instantiate(_template);
        newObject.gameObject.SetActive(false);
        Pool.Add(newObject);
    }
}