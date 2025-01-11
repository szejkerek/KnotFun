using System;
using UnityEngine;
using UnityEngine.UI;

public enum MapNodeUIStates
{
    Locked,
    Visited,
    Attainable
}

public class MapNodeUI : MonoBehaviour
{
    const int k_startLevelIndex = SceneConstants.Level_1;
    [Header("Level Loader")]
    [SerializeField] int levelIndex;
    [SerializeField] Sprite[] islands;

    [SerializeField] SoundOld buttonIslandHover = null;
    [SerializeField] SoundOld buttonIslandHoverUnactive = null;
    [SerializeField] SoundOld buttonIslandPressed = null;
    Button button;

    public SpriteRenderer sr;
    public Image icon;
    public Image islandImage;
    public MapNode mapNode { get; private set; }
    public MapNodeTypeSO mapNodeType { get; private set; }

    private MapNodeUIStates mapNodeUIState;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => TryEnterNode());
    }

    public void SetUp(MapNode node, MapNodeTypeSO mapNodeType, int index)
    {
        mapNode = node;
        this.mapNodeType = mapNodeType;
        if (sr != null) sr.sprite = mapNodeType.Icon;
        if (icon != null) icon.sprite = mapNodeType.IslandImage;
        if (icon != null) islandImage.sprite = islands[UnityEngine.Random.Range(0, islands.Length)];//mapNodeType.IslandImage;
        if (node.type == MapNodeType.Boss) { transform.localScale *= 1.5f; }//GetComponent<Renderer>().enabled !visual.enabled = null; }
        levelIndex = index;
        SetState(MapNodeUIStates.Locked);
    }

    public void SetState(MapNodeUIStates state)
    {
        mapNodeUIState = state;
        switch (state)
        {
            case MapNodeUIStates.Locked:
                if (sr != null)
                {
                    sr.color = MapDrawerUI.Instance.lockedColor;
                }

                if (islandImage != null)
                {
                    islandImage.color = MapDrawerUI.Instance.lockedColor;
                    icon.color = Color.gray;
                }

                break;
            case MapNodeUIStates.Visited:
                if (sr != null)
                {
                    sr.color = MapDrawerUI.Instance.visitedColor;
                }

                if (islandImage != null)
                {
                    islandImage.color = MapDrawerUI.Instance.visitedColor;
                    icon.gameObject.SetActive(false);
                }
                break;
            case MapNodeUIStates.Attainable:
                // start pulsating from visited to locked color:
                if (sr != null)
                {
                    sr.color = MapDrawerUI.Instance.attainableColor;
                }

                if (islandImage != null)
                {
                    islandImage.color = MapDrawerUI.Instance.attainableColor;
                    icon.color = Color.black;//MapDrawerUI.Instance.visitedColor;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void TryEnterNode()
    {
        if (mapNodeUIState == MapNodeUIStates.Attainable)
        {
            AudioManagerOld.Instance.PlayGlobal(buttonIslandPressed);
            MapPlayerTracker.Instance.SendPlayerToNode(this);
        }
    }

    public void LoadNodeLevel()
    {
        //TODO LOAD SCENE THAT WE WANT
        //int currentIndex = levelIndex + k_startLevelIndex;
        //SceneLoader.Instance.LoadScene(currentIndex);


    }

    public void ButtonOnHover()
    {
        if (mapNodeUIState == MapNodeUIStates.Attainable)
        {
            AudioManagerOld.Instance.PlayGlobal(buttonIslandHover);
        }
        else
            AudioManagerOld.Instance.PlayGlobal(buttonIslandHoverUnactive);
    }

}

