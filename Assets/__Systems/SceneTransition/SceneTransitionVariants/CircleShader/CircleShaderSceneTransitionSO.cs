using PlaceHolders.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders.SceneTransitions
{
    [CreateAssetMenu(fileName = "CircleShader", menuName = "Scene Transition/CircleShader")]
    public class CircleShaderSceneTransitionSO : AbstractSceneTransitionSO
    {
        public Material material;
        public string progressPropertyName;

        public override IEnumerator Enter(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.material = material;
            animatedObject.rectTransform.anchorMin = Vector3.zero;
            animatedObject.rectTransform.anchorMax = Vector2.one;
            animatedObject.rectTransform.sizeDelta = Vector2.zero;
            float progres = 0;

            while (progres < 1)
            {
                material.SetFloat(progressPropertyName, 1 - progres);
                yield return null;
                progres += Time.deltaTime / animationTime;
                //Debug.Log(progres);
            }

            Destroy(animatedObject.gameObject);
        }

        public override IEnumerator Exit(Canvas parent)
        {
            animatedObject = CreateImage(parent);
            animatedObject.material = material;
            animatedObject.rectTransform.anchorMin = Vector3.zero;
            animatedObject.rectTransform.anchorMax = Vector2.one;
            animatedObject.rectTransform.sizeDelta = Vector2.zero;

            float progres = 0;

            while (progres < 1)
            {
                material.SetFloat(progressPropertyName, progres);
                yield return null;
                progres += Time.deltaTime / animationTime;

            }
            Destroy(animatedObject.gameObject);
        }
    }
}
