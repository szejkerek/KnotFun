using System.Collections.Generic;
using UnityEngine;

public class SamuraiRenderers : MonoBehaviour
{
    public SpriteRenderer Helmet_B;
    public SpriteRenderer Helmet_F;
    public SpriteRenderer Face_F;
    public SpriteRenderer Head;
    public SpriteRenderer Weapon;
    public SpriteRenderer Chestplate;
    public SpriteRenderer Pants;

    private List<SpriteRenderer> renderers;

    private void Awake()
    {
        renderers = new List<SpriteRenderer>
        {
            Helmet_B,
            Helmet_F,
            Face_F,
            Head,
            Weapon,
            Chestplate,
            Pants
        };

        float f = 0;
        float u = Random.Range(0f, 1f);
        float v = Random.Range(0f, 1f);
        foreach (SpriteRenderer r in renderers)
        {
            r.material.SetFloat("_Damage", f);
            r.material.SetVector("_Shift", new Vector2(u, v));
        }
    }

    public void SetDamagePercent(float percentage)
    {
        foreach (SpriteRenderer r in renderers)
        {
            r.material.SetFloat("_Damage", percentage);
        }
    }
}
