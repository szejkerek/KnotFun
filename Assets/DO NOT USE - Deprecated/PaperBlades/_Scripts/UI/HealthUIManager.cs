using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Canvas canvas;
    float yOffset = 140f;
    // Start is called before the first frame update
    void Start()
    {


        SpawnHealthBar(SavableDataManager.Instance.data.team.General, 0, new Color(255f / 255, 73f / 255, 73f / 255, 255f / 255));

        for (int i = 1; i <= SavableDataManager.Instance.data.team.TeamMembers.Count; i++)
        {
            SpawnHealthBar(SavableDataManager.Instance.data.team.TeamMembers[i - 1], i, new Color(1f, 1f, 1f, 1f));
        }
    }

    void SpawnHealthBar(Character character, int position, Color colorC)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector3 startPosition = new Vector3(canvasRect.rect.xMin + 220, canvasRect.rect.yMax - 60, 0);


        Vector3 spawnPosition = startPosition - new Vector3(0, yOffset * position, 0);


        GameObject spawnedPrefab = Instantiate(healthBarPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        HealthBar hb = spawnedPrefab.GetComponent<HealthBar>();
        hb.Init(character);
        hb.characterUIDisplay.SetColor(colorC);
        RectTransform spawnedRect = spawnedPrefab.GetComponent<RectTransform>();
        if (spawnedRect != null)
        {
            spawnedRect.anchoredPosition = new Vector2(startPosition.x, startPosition.y - yOffset * position);
        }
    }
}
