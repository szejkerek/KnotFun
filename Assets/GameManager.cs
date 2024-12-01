using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}