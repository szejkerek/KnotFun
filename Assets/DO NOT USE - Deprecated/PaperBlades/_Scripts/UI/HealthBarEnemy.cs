using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public int maxHealth = 100;
    public int health;
    public float lerpSpeed = 0.05f;
    Enemy Enemy;
    Camera mainCam;
    public void Init(Enemy enemy)
    {
        mainCam = Camera.main;
        Enemy = enemy;
        maxHealth = enemy.GetStats().MaxHealth;
        health = enemy.Health;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        easeHealthSlider.maxValue = maxHealth;
        easeHealthSlider.value = health;
        enemy.OnDamaged += UpdateHealthBar;
    }

    void UpdateHealthBar(Enemy enemy)
    {
        health = enemy.Health;
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
    }

    void Update()
    {
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

        if (mainCam != null)
        {
            transform.LookAt(mainCam.transform);
            transform.forward = -transform.forward;
        }
    }
}
