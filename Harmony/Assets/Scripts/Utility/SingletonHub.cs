using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class SingletonHub : MonoBehaviour
{
    public static SingletonHub Instance { get; private set; }

    [SerializeField] private List<string> _registeredSingletons = new List<string>();
    private Dictionary<System.Type, MonoBehaviour> _singletons = new Dictionary<System.Type, MonoBehaviour>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Register<T>(T service) where T : MonoBehaviour
    {
        var type = typeof(T);
        if (!_singletons.ContainsKey(type))
        {
            _singletons[type] = service;
            _registeredSingletons.Add(type.Name);
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        var type = typeof(T);
        if (_singletons.TryGetValue(type, out var service))
        {
            return (T)service;
        }

        return null;
    }
}
