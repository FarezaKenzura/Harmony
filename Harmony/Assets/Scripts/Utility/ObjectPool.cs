using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pooled
{
    public GameObject Prefab;
    public Queue<GameObject> PoolQueue = new Queue<GameObject>();
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<Pooled> _pooledList = new List<Pooled>();

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    private Pooled FindOrCreatePool(GameObject prefab)
    {
        foreach (var p in _pooledList)
        {
            if (p.Prefab == prefab)
                return p;
        }

        Pooled newPool = new Pooled { Prefab = prefab };
        _pooledList.Add(newPool);
        return newPool;
    }

    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        Pooled pooled = FindOrCreatePool(prefab);

        if (pooled.PoolQueue.Count > 0)
        {
            GameObject obj = pooled.PoolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(parent);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab, position, rotation, parent);
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        foreach (var p in _pooledList)
        {
            if (obj.name.StartsWith(p.Prefab.name))
            {
                obj.SetActive(false);
                p.PoolQueue.Enqueue(obj);
                return;
            }
        }

        Destroy(obj);
    }
}
