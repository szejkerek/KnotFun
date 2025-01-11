using UnityEngine;
using UnityEngine.UI;
public enum CursorState
{
    Default,
    Attack,
    UIHover,
    UseMoney,
    Tooltip,
}

public class CursorManager : Singleton<CursorManager>
{
    public Image cursorSprite;
    public Slider slider;
    [Space]
    public Sprite Default;
    public Sprite Attack;
    public Sprite UIHover;
    public Sprite UseMoney;
    public Sprite Tooltip;
    public Vector2 cursorOffset;

    void Start()
    {
        Cursor.visible = false;
        SetCursorState(CursorState.Default);
        slider.gameObject.SetActive(false);
    }
    public void SetCursorState(CursorState state)
    {
        switch (state)
        {
            case CursorState.Default:
                cursorSprite.sprite = Default;
                break;
            case CursorState.Attack:
                cursorSprite.sprite = Attack;
                break;
            case CursorState.UIHover:
                cursorSprite.sprite = UIHover;
                break;
            case CursorState.UseMoney:
                cursorSprite.sprite = UseMoney;
                break;
            case CursorState.Tooltip:
                cursorSprite.sprite = Tooltip;
                break;
        }
    }

    bool shouldCountDown;
    float currentTimer = 0;
    float timer = 0;
    public void SetCooldownOnCursor(float time)
    {
        slider.gameObject.SetActive(true);
        slider.value = 1f;
        currentTimer = time;
        timer = time;

        if (timer > 0)
        {
            shouldCountDown = true;
        }
    }

    void Update()
    {
        Vector2 cursorPosition = Input.mousePosition;
        cursorSprite.transform.position = cursorPosition + cursorOffset;

        if (shouldCountDown)
        {
            currentTimer -= Time.deltaTime;
            float ratio = currentTimer / timer;
            slider.value = ratio;

            if (ratio <= 0f)
            {
                slider.gameObject.SetActive(false);
                shouldCountDown = false;
            }
        }
    }
}
