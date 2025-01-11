
using UnityEngine;

public class SamuraiStylizer : MonoBehaviour
{
    public SamuraiRenderers Renderers { get; private set; }



    private void Awake()
    {
        Renderers = GetComponentInChildren<SamuraiRenderers>();

    }


}