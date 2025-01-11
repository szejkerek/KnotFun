using UnityEngine;

public class UpgradableSegment : MonoBehaviour
{
    [SerializeField] GameObject activeGameObject, inactiveGameObject;

    void Awake() => Deavtivate();

    public void Activate()
    {
        activeGameObject.SetActive(true);
        inactiveGameObject.SetActive(false);
    }

    public void Deavtivate()
    {
        activeGameObject.SetActive(false);
        inactiveGameObject.SetActive(true);
    }
}
