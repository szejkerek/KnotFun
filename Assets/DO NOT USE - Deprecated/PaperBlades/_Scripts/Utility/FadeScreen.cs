using System;
using System.Collections;
using UnityEngine;


public class FadeScreen : Singleton<FadeScreen>
{
    [SerializeField] private bool fadeOnStart;

    [SerializeField] FadeScreenTarget target;
    void Start()
    {
        target = FindObjectOfType<FadeScreenTarget>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn(float duration = 0.75f) => StartCoroutine(FadeCoroutine(1, 0, duration));

    public void FadeOut(float duration = 0.75f) => StartCoroutine(FadeCoroutine(0, 1, duration));

    public void FadeAction(Action action, float duration = 1.5f)
    {
        StartCoroutine(FadeActionCoroutine(action, duration));
    }

    private IEnumerator FadeActionCoroutine(Action action, float duration = 1.5f)
    {
        yield return FadeCoroutine(0, 1, duration * 0.45f);

        action?.Invoke();

        yield return new WaitForSeconds(duration * 0.1f);

        yield return FadeCoroutine(1, 0, duration * 0.45f);
    }

    private IEnumerator FadeCoroutine(float alphaIn, float alphaOut, float duration = 0.75f)
    {
        duration = Mathf.Max(0.001f, duration);
        float timer = 0f;
        while (timer < duration)
        {
            target.SetAlpha(Mathf.Lerp(alphaIn, alphaOut, timer / duration));

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        target.SetAlpha(alphaOut);
    }
}


