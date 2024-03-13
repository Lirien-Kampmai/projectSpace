using UnityEngine;

/// <summary>
/// Base script that allows you to make any other child script a Singleton.
/// </summary>
[DisallowMultipleComponent]
public abstract class SingletonBase <T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool doNotDestroyOnLoad;

    public static T Instance { get; private set; }

    protected virtual void Awake ()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance wii be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }
        Instance = this as T;

        if (doNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}