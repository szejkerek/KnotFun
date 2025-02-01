using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlaceHolders.SceneTransitions
{
    [CreateAssetMenu(fileName = "CircleCode", menuName = "Scene Transition/CircleCode")]
    public class CircleCodeSceneTransitionSO : AbstractSceneTransitionSO
    {
        public Sprite circleSprite;
        public Color color;

        public override IEnumerator Enter(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.color = color;
          
            float progres = 0;
            float size = Mathf.Sqrt(Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2));
            Vector2 initialSize = new Vector2(size, size);

            animatedObject.rectTransform.sizeDelta = initialSize;
            animatedObject.sprite = circleSprite;
            while (progres < 1)
            {
                animatedObject.rectTransform.sizeDelta = Vector2.Lerp(initialSize, Vector2.zero, progres);
                yield return null;
                progres += Time.deltaTime / animationTime;

            }

            Destroy(animatedObject.gameObject);
        }

        public override IEnumerator Exit(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.color = color;
            animatedObject.rectTransform.sizeDelta = Vector2.zero;
            animatedObject.sprite = circleSprite;

            float progres = 0;
            float size = Mathf.Sqrt(Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2));
            Vector2 targetSize = new Vector2(size, size);

            while (progres < 1)
            {
                animatedObject.rectTransform.sizeDelta = Vector2.Lerp(Vector2.zero, targetSize, progres);
                yield return null;
                progres += Time.deltaTime / animationTime;

            }
            Destroy(animatedObject.gameObject);
        }
    }
}
