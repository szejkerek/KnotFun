using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlaceHolders.SceneTransitions
{
    public abstract class AbstractSceneTransitionSO : ScriptableObject
    {
        public float animationTime = 0.75f;
        protected Image animatedObject;

        public abstract IEnumerator Enter(Canvas parent);
        public abstract IEnumerator Exit(Canvas parent);

        protected virtual Image CreateImage(Canvas parent)
        {
            GameObject child = new GameObject("SceneTransitions Image");
            child.transform.SetParent(parent.transform, false);

            return child.AddComponent<Image>();
        }

    }
}
