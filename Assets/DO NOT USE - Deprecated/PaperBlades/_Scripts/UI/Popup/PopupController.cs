using UnityEngine;

public class PopupController : Singleton<PopupController>
{
    public PopupWindowPanel PopupPanel => popupPanel;
    [SerializeField] private PopupWindowPanel popupPanel;
}
