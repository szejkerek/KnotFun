using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    string tempName = typeof(T).ToString();
                    GameObject go = GameObject.Find(tempName);

                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = tempName;
                    }
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }

            return instance;
        }
    }
}