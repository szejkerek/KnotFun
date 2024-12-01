using UnityEngine;

public class BulletTimeManager : MonoBehaviour
{
    private int activeCharacters = 0;
    public static BulletTimeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adjust active character count and recalculate time scale
    public void SetCharacterActive(bool isActive)
    {
        if (isActive)
        {
            activeCharacters++;
        }
        else
        {
            activeCharacters = Mathf.Max(0, activeCharacters - 1);
        }

        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        // Adjust time scale based on the number of active characters
        switch (activeCharacters)
        {
            case 0:
                Time.timeScale = 0f; // All characters standing
                break;
            case 1:
                Time.timeScale = 0.33f;
                break;
            case 2:
                Time.timeScale = 0.66f;
                break;
            default:
                Time.timeScale = 1f; // All characters active
                break;
        }

        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust fixed delta time
    }
}