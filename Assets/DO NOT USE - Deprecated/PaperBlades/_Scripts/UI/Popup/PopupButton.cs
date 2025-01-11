using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupButton : MonoBehaviour
{
    Button button;
    TMP_Text buttonText;

    Action customAction;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        button.gameObject.SetActive(false);
    }

    public void Init(string text, Action action)
    {
        customAction = action;
        buttonText.text = text;
        button.gameObject.SetActive(true);
        button.onClick.AddListener(TriggerAction);
    }

    public void Deinit()
    {
        customAction = null;
        button.gameObject.SetActive(false);
        buttonText.text = string.Empty;
        button.onClick.RemoveListener(TriggerAction);
    }

    private void OnDisable()
    {
        Deinit();
    }

    void TriggerAction()
    {
        customAction?.Invoke();
    }
}
