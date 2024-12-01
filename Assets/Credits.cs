using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject credits;


    public ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
    private Vector3 lastPosition;


    private void Start()
    {
        lastPosition = transform.position;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || gamepads[0].xButton.isPressed || gamepads[1].xButton.isPressed)
        {
            Debug.Log("Start game");
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) || gamepads[0].aButton.isPressed || gamepads[1].aButton.isPressed)
        {
            Debug.Log("credits");

            if (credits.active)
            {
                credits.SetActive(false);
                menu.SetActive(true);
            }
            else
            {
                credits.SetActive(true);
                menu.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape) || gamepads[0].startButton.isPressed || gamepads[1].startButton.isPressed)
        {
            Exit();
        }

    }

    public void ChangeToCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeToMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }
}
