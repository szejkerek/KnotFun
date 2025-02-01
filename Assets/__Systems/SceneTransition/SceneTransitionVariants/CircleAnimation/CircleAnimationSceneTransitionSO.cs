using PlaceHolders.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders.SceneTransitions
{
    [CreateAssetMenu(fileName = "CircleAnimation", menuName = "Scene Transition/CircleAnimation")]
    public class CircleAnimationSceneTransitionSO : AbstractSceneTransitionSO
    {
        public Animator animator;
        public GameObject objectsToAnimate;
        public string clipNameEnter;
        public string clipNameExit;
        private GameObject animationObjects;

        public override IEnumerator Enter(Canvas parent)
        {
            animationObjects = Instantiate(objectsToAnimate, parent.transform);
            animator = animationObjects.GetComponent<Animator>();
            animator.Play(clipNameEnter, 0,0.0f);
            float progres = 0f;
            while (progres < 1)
            {
                yield return null;
                progres += Time.deltaTime / animationTime;

            }

            Destroy(animationObjects.gameObject);
        }

        public override IEnumerator Exit(Canvas parent)
        {
            animationObjects = Instantiate(objectsToAnimate, parent.transform);
            animator = animationObjects.GetComponent<Animator>();
            animator.Play(clipNameExit, 0, 0.0f);
            float progres = 0f;
            while (progres < 1)
            {
                yield return null;
                progres += Time.deltaTime / animationTime;

            }
            Destroy(animationObjects.gameObject);
        }
    }
}
