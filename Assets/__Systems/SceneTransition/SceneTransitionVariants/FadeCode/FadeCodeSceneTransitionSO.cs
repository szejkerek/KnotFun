using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders.SceneTransitions
{
    [CreateAssetMenu(fileName = "FadeCode", menuName = "Scene Transition/FadeCode")]
    public class FadeCodeSceneTransitionSO : AbstractSceneTransitionSO
    {
        public override IEnumerator Enter(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.rectTransform.anchorMin = Vector3.zero;
            animatedObject.rectTransform.anchorMax = Vector2.one;
            animatedObject.rectTransform.sizeDelta = Vector2.zero;
            float progres = 0;
            Color startColor = Color.black;
            Color endColor = new Color(0, 0, 0, 0);
            while (progres < 1)
            {
                animatedObject.color = Color.Lerp(startColor, endColor, progres);
                yield return null;
                progres += Time.deltaTime / animationTime;

            }

            Destroy(animatedObject.gameObject);
        }

        public override IEnumerator Exit(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.rectTransform.anchorMin = Vector3.zero;
            animatedObject.rectTransform.anchorMax = Vector2.one;
            animatedObject.rectTransform.sizeDelta = Vector2.zero;

            float progres = 0;
            Color startColor = new Color(0, 0, 0, 0);
            Color endColor = Color.black;

            while (progres < 1)
            {
                animatedObject.color = Color.Lerp(startColor, endColor, progres);
                yield return null;
                progres += Time.deltaTime / animationTime;

            }
            Destroy(animatedObject.gameObject);
        }
    }
}
