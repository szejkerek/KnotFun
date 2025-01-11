using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFadeScreenTarget : FadeScreenTarget
{
    [SerializeField] Color fadeColor = Color.black;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        SetAlpha(0);
    }

    public override void SetAlpha(float target)
    {
        target = Mathf.Clamp01(target);
        Color newColor = fadeColor;
        newColor.a = target;
        image.color = newColor;
    }
}
