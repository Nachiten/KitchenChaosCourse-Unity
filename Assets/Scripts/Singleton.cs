/**
 * Singleton.cs
 * Author: Luke Holland (http://lukeholland.me/)
 **/

using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _instanceLock = new();
    private static bool _quitting;

    public static T Instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance != null || _quitting)
                    return _instance;

                _instance = FindObjectOfType<T>();

                if (_instance != null)
                    return _instance;

                GameObject go = new(typeof(T).ToString());
                _instance = go.AddComponent<T>();

                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>();
        }

        else if (_instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
            throw new Exception($"Instance of {GetType().FullName} already exists, removing {ToString()}");
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _quitting = true;
    }
}