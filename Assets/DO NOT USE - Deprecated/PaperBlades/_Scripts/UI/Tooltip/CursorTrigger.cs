using UnityEngine;
using UnityEngine.EventSystems;

public class CursorTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CursorState state;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursorState(state);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursorState(CursorState.Default);
    }

    private void OnMouseEnter()
    {
        CursorManager.Instance.SetCursorState(state);
    }

    private void OnMouseExit()
    {
        CursorManager.Instance.SetCursorState(CursorState.Default);
    }
}
