using UnityEngine;

public class InfinityParalel : MonoBehaviour
{
    public Transform P1;
    public Transform P2;

    void Update()
    {
        transform.forward = P2.position - P1.position;
    }
}
