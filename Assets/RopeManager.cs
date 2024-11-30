using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private Rope rope;
    [SerializeField] private GameObject ropeLoop;
    
    private float currentRopeLenght;

    public void SetVisiblebool(bool active)
    {
        rope.gameObject.SetActive(active);
        rope.RecalculateRope();
    }
    
    public void ChangeLineLenght(float lenght)
    {
        StopAllCoroutines();
        currentRopeLenght = lenght;
        rope.ropeLength = currentRopeLenght;
        rope.RecalculateRope();
    }
}
