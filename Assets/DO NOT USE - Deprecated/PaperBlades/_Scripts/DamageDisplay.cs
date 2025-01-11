using System.Collections;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    TMP_Text damageText;
    public Interval<int> dmgBounds;

    public Color bottom;
    public Color top;
    public Interval<float> fontSize;

    Camera mainCam;

    public float unitsAway;

    public SoundOld[] damageSound;
    //public Sound damage;

    public float moveSpeed = 1.0f;
    public float fadeDuration = 1.0f;

    void Awake()
    {
        mainCam = Camera.main;
        damageText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        if (mainCam != null)
        {
            transform.LookAt(mainCam.transform);
            transform.forward = -transform.forward;
        }
    }

    public void Init(int damage, Vector3 origin, Vector3 target)
    {
        AudioManagerOld.Instance.PlayAtPosition(this.transform.position, damageSound[Random.RandomRange(0, damageSound.Length - 1)]);

        damageText.text = damage.ToString();
        float t = (float)(damage - dmgBounds.BottomBound) / (dmgBounds.UpperBound - dmgBounds.BottomBound);
        damageText.color = Color.Lerp(bottom, top, t);

        damageText.fontSize *= Mathf.Lerp(fontSize.BottomBound, fontSize.UpperBound, t);  // Set to interpolated font size

        Vector3 toTargetDir = (target - origin) * 0.5f;
        transform.position = origin + toTargetDir;

        StartCoroutine(FlyAndFade());
    }

    IEnumerator FlyAndFade()
    {
        float elapsedTime = 0f;
        Color originalColor = damageText.color;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < fadeDuration)
        {
            transform.position = originalPosition + Vector3.up * moveSpeed * elapsedTime;
            float alpha = Mathf.Lerp(255.0f, 0f, elapsedTime / fadeDuration);
            damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
