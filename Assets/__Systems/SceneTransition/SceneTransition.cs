using System;
using UnityEngine;

namespace PlaceHolders.SceneTransitions
{
    [Serializable]
    public class SceneTransition
    {
        public SceneTransitionType mode;
        public AbstractSceneTransitionSO transitionSO;
    }
    
}
