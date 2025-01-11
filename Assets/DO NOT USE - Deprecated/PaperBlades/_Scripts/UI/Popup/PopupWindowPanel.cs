using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopupWindowPanel : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private Transform choicesParent;
    [SerializeField] private ChoiceUI choiceUIPrefab;

    [Header("Header")]
    [SerializeField] private Transform headerArea;
    [SerializeField] private TextMeshProUGUI headerText;

    [Header("Content")]
    [SerializeField] private Transform verticalLayoutArea;
    [SerializeField] private TextMeshProUGUI verticalLayoutText;
    [Space]
    [SerializeField] private Transform horizontalLayoutArea;
    [SerializeField] private Image horizontalLayoutImage;
    [SerializeField] private TextMeshProUGUI horizontalLayoutText;

    [Header("Footer")]
    [SerializeField] private PopupButton confirmButton;
    [SerializeField] private PopupButton declineButton;
    [SerializeField] private PopupButton alternateButton;

    [Header("Sounds")]
    [SerializeField] SoundOld buttonPressed = null;
    [SerializeField] SoundOld buttonHover = null;
    [SerializeField] SoundOld eventAmbient = null;
    [SerializeField] SoundOld paperEffect = null;
    [SerializeField] SoundOld backToMenu = null;

    CanvasGroup canvasGroup;
    Character choiceItem;
    List<ChoiceUI> listOfChoices;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Deinit();
        Close();
    }
    private void Start()
    {
        AudioManagerOld.Instance.PlayGlobal(paperEffect);
        AudioManagerOld.Instance.PlayGlobal(eventAmbient, SoundType.Music);
    }

    public void ChooseModal(List<Character> elements, Action<Character> OnCharacterSelected, string title = null, string content = null, Action confirmAction = null, Action declineAction = null, Action alternateAction = null)
    {
        Show(title, horizontal: false);
        verticalLayoutText.text = content;
        FillChoices(elements);

        confirmAction += () =>
        {
            if (choiceItem != null)
            {
                OnCharacterSelected?.Invoke(choiceItem);
            }
            Close();
        };

        declineAction += () =>
        {
            Close();
        };

        SetupButtons(confirmAction, declineAction, alternateAction);
    }

    public void ChooseModal(PassiveEffectSO passiveEffect, List<Character> elements, Action<Character, PassiveEffectSO> OnCharacterSelected, string title = null, string content = null, Action confirmAction = null, Action declineAction = null, Action alternateAction = null)
    {
        Show(title, horizontal: false);
        verticalLayoutText.text = content;
        FillChoices(elements);

        confirmAction += () =>
        {
            if (choiceItem != null)
            {
                OnCharacterSelected?.Invoke(choiceItem, passiveEffect);
            }
            Close();
        };

        declineAction += () =>
        {
            Close();
        };

        SetupButtons(confirmAction, declineAction, alternateAction);
    }





    public void ShowAsEvent(PopupEventSO eventData, Action confirmAction = null, Action declineAction = null, Action alternateAction = null)
    {
        Show(eventData.Header, horizontal: true);
        horizontalLayoutText.text = eventData.Content;
        horizontalLayoutImage.sprite = eventData.DisplayImage;

        confirmAction += () =>
        {
            eventData.OnAccept.Apply();
            Close();
        };

        declineAction += () =>
        {
            eventData.OnDecline.Apply();
            Close();
        };

        SetupButtons(confirmAction, declineAction, alternateAction, eventData.answerOne, eventData.answerTwo);
    }


    private void FillChoices(List<Character> elements)
    {
        listOfChoices = new List<ChoiceUI>();
        ChoiceUI.OnCharacterSelected += SetChoice;
        for (int i = 0; i < elements.Count; i++)
        {
            var choice = Instantiate(choiceUIPrefab, choicesParent);
            listOfChoices.Add(choice);
            choice.Init(elements[i]);
        }
    }

    private void SetupButtons(Action confirmAction, Action declineAction, Action alternateAction, string sConfirm = "Confirm", string sDecline = "Decline")
    {
        InitializeButton(confirmButton, "Confirm", confirmAction);
        confirmButton.GetComponentInChildren<TMP_Text>().text = sConfirm == "" ? "Confirm" : sConfirm;
        InitializeButton(alternateButton, "Alternate", alternateAction);
        InitializeButton(declineButton, "Decline", declineAction);
        declineButton.GetComponentInChildren<TMP_Text>().text = sDecline == "" ? "Decline" : sDecline;
    }

    private void InitializeButton(PopupButton button, string label, Action action)
    {
        if (action != null)
        {
            button.Init(label, action.Invoke);
        }
    }

    private void Show(string title, bool horizontal)
    {
        horizontalLayoutArea.gameObject.SetActive(horizontal);
        verticalLayoutArea.gameObject.SetActive(!horizontal);


        headerArea.gameObject.SetActive(title != "");
        headerText.text = title;


        gameObject.SetActive(true);
        DOTween.Sequence()
        .Append(canvasGroup.DOFade(1, 0.75f))
        .Join(transform.DOScale(new Vector3(0.5f, 0.45f, 1.0f), 0.75f).SetEase(Ease.OutBounce))
        .Play();
    }

    private void Close()
    {
        ChoiceUI.OnCharacterSelected -= SetChoice;
        if (MapPlayerTracker.Instance != null) MapPlayerTracker.Instance.locked = false;

        AudioManagerOld.Instance.PlayGlobal(backToMenu, SoundType.Music);

        DOTween.Sequence()
        .Append(canvasGroup.DOFade(0, 0.5f))
        .Join(transform.DOScale(Vector3.zero, 0.5f))
        .OnComplete(Deinit)
        .Play();
    }

    private void Deinit()
    {
        gameObject.SetActive(false);

        if (listOfChoices != null)
        {
            foreach (var item in listOfChoices)
            {
                Destroy(item.gameObject);
            }
            listOfChoices.Clear();
            listOfChoices = null;
        }
    }

    private void SetChoice(Character choice)
    {
        choiceItem = choice;
    }

    public void ButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }
}
