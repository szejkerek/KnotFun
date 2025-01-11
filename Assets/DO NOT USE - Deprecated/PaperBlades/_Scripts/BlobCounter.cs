using TMPro;
using UnityEngine;

public class BlobCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    int currentCount = 0;
    int MaxEnemies;

    public void Init(int maxEnemies)
    {
        MaxEnemies = maxEnemies;
        Enemy.OnEnemyKilled += IncrementCounter;
        SetDisplayText(0);
    }

    private void IncrementCounter(Enemy enemy)
    {
        currentCount++;
        SetDisplayText(currentCount);
    }

    private void SetDisplayText(int currentCount)
    {
        text.text = $"{currentCount}/{MaxEnemies}";
    }
}
