using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public int maxHealth = 100;
    public int health;
    public float lerpSpeed = 0.05f;
    public Character currCharacter;
    public CharacterUIDisplay characterUIDisplay;

    // Start is called before the first frame update
    public void Init(Character character)
    {
        currCharacter = character;
        characterUIDisplay.Init(currCharacter);
        maxHealth = currCharacter.GetStats().MaxHealth;
        health = maxHealth - currCharacter.LostHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        easeHealthSlider.maxValue = maxHealth;
        easeHealthSlider.value = health;
        currCharacter.OnHealthChange += UpdateHealthBar;
    }

    void UpdateHealthBar()
    {
        health = maxHealth - currCharacter.LostHealth;
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

    }
}
