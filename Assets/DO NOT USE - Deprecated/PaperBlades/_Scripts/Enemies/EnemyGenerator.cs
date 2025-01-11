using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Material baseMaterial;
    private Material newMaterial;
    public MeshRenderer renderer;

    public List<Transform> transforms;
    private List<Vector3> initialSizes;
    private float overallDamage;
    public Interval<float> scaleModifier;


    public void Awake()
    {
        newMaterial = Instantiate(baseMaterial);
        renderer.materials[0] = newMaterial;
        initialSizes = new List<Vector3>();

        foreach (Transform t in transforms)
        {
            t.localScale = new Vector3(t.localScale.x * scaleModifier.GetValueBetween(), t.localScale.y * scaleModifier.GetValueBetween(), t.localScale.z * scaleModifier.GetValueBetween());
            initialSizes.Add(t.localScale);
        }
    }


    public void GetDamage(float amount)
    {
        amount += 0.4f;
        if (amount > 1) amount = 1;

        for (int i = 0; i < transforms.Count; i++)
        {
            transforms[i].localScale = new Vector3(initialSizes[i].x * amount, initialSizes[i].y, initialSizes[i].z * amount);
        }
    }


    public void Update()
    {
        //GetDamage(1 - Time.deltaTime * 0.1f);
        //newMaterial.SetFloat("_Smooth", 0.5f);
    }
}
