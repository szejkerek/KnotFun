using PlaceHolders.EventChannel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlaceHolders.SceneTransitions
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private Canvas transitionCanvas;
        [SerializeField] private Canvas progressCanvas;
        [SerializeField] private Image progressImage;
        [SerializeField] private List<SceneTransition> transitions = new List<SceneTransition>();
        [SerializeField] private FloatEventListener ProgressEventListener;
        public AbstractSceneTransitionSO activeTransition;
        

        private Queue<float> progressesQueue = new Queue<float>();
        
        void Awake()
        {
            ProgressEventListener.UnityEventResponse.AddListener(AddProgressValue);
            transitionCanvas.enabled = false;
            progressCanvas.enabled = false;
        }

        public void AddProgressValue(float progress)
        {
            progressesQueue.Enqueue(progress);
        }

        public IEnumerator MakeTransition(SceneTransitionType transitionMode = SceneTransitionType.None, bool enterTranstition = true, bool progressScreen = false)
        {
            SceneTransition transition = transitions.Find((transition) => transition.mode == transitionMode);
            
            if (transition != null)
            {
                
                if (enterTranstition)
                {
                    yield return StartCoroutine(Enter());
                }
                else
                {
                    activeTransition = transition.transitionSO;
                    yield return StartCoroutine(Exit());
                    if(progressScreen) yield return StartCoroutine(ProgressScreen());
                }
            }
            else
            {
                Debug.Log($"No transition for choosen SceneTransitionType {transitionMode}");
                yield return null;
            }
        }

        private IEnumerator ProgressScreen()
        {
            progressCanvas.enabled = true;
            float actualProgress = 0;
            while (progressesQueue.Count > 0 || actualProgress < 0.9f)
            {   
                if(progressesQueue.Count > 0) actualProgress = progressesQueue.Dequeue();
                //progressText.text = actualProgress.ToString();
                progressImage.fillAmount = actualProgress / 0.9f;
                yield return null;
            }
            progressCanvas.enabled = false;
        }

        private IEnumerator Enter()
        {
            yield return StartCoroutine(activeTransition.Enter(transitionCanvas));
            transitionCanvas.enabled = false;
            activeTransition = null;
        }

        private IEnumerator Exit()
        {
            transitionCanvas.enabled = true;
            yield return StartCoroutine(activeTransition.Exit(transitionCanvas));
        }
    }

    
}
