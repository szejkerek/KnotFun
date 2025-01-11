public class TooltipSystem : Singleton<TooltipSystem>
{
    public Tooltip tooltip;

    private void Start()
    {
        Hide();
    }

    public static void Show(string content, string header = "")
    {
        Instance.tooltip.SetText(content, header);
        Instance.tooltip.gameObject.SetActive(true);
        CursorManager.Instance.SetCursorState(CursorState.Tooltip);
    }

    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
        CursorManager.Instance.SetCursorState(CursorState.Default);
    }
}
