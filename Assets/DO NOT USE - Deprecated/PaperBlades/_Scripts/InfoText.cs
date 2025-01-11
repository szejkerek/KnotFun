using System.Collections;
using TMPro;
using UnityEngine;

public enum InfoLenght
{
    Short,
    Medium,
    Long
}

public class InfoText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    float disolveTime;

    public void Init(string text, InfoLenght lenght)
    {
        this.text.text = text;

        switch (lenght)
        {
            case InfoLenght.Short:
                disolveTime = 0.35f;
                break;
            case InfoLenght.Medium:
                disolveTime = 1f;
                break;
            case InfoLenght.Long:
                disolveTime = 3.5f;
                break;
        }
    }


    public IEnumerator Dissolver()
    {
        // Wait for the dissolve time
        yield return new WaitForSeconds(disolveTime);

        // Get the initial color of the text
        Color initialColor = text.color;

        // Time it takes to fade out
        float fadeDuration = 0.75f;
        float elapsedTime = 0;

        // Gradually reduce the alpha to 0
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Ensure the alpha is set to 0 at the end
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
