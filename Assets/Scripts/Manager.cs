using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Component, new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!ReferenceEquals(_instance, null)) return _instance;
            _instance = FindObjectOfType<T>();
            if (!ReferenceEquals(_instance, null)) return _instance;
            var obj = new GameObject();
            _instance = obj.AddComponent<T>();
            obj.name = typeof(T).Name;
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            Prepare();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Prepare()
    { }
}
