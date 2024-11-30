using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject credits;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    public void ChangeToCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void ChangeToMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }
}
