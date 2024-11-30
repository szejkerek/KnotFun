using UnityEngine;

public class TextCrawler : MonoBehaviour
{
    [SerializeField] GameObject textCredits;
    [SerializeField] private float _scrollSpeed = 20f;

    [SerializeField] Vector3 initialPosition;

    private void OnEnable()
    {
        initialPosition.x = 0;
        initialPosition.y = -347;
        initialPosition.z = -320;
        textCredits.transform.position = initialPosition;
    }
    void Update()
    {
        if (textCredits != null)
        {
            textCredits.transform.position += Camera.main.transform.up * _scrollSpeed * Time.deltaTime;

        }
    }
}
