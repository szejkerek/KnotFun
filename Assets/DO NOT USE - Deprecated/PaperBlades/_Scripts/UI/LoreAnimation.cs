using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoreAnimation : MonoBehaviour
{
    [Header("Part 1")]
    [SerializeField] private Image lorePart1Img;
    [SerializeField] private TextMeshProUGUI lorePart1Text;

    [Header("Part 2")]
    [SerializeField] private Image lorePart2Img;
    [SerializeField] private TextMeshProUGUI lorePart2Text;

    [Header("Part 3")]
    [SerializeField] private Image lorePart3Img;
    [SerializeField] private TextMeshProUGUI lorePart3Text;

    [Header("Start Button")]
    [SerializeField] private Image startButtonImg;
    [SerializeField] private TextMeshProUGUI startButtonText;

    [Header("Timing Tweaking")]
    public float startupDelay = .75f;
    public float imageAnimationTime = .5f;
    public float textAnimationTime = 1f;
    public float segmentDelay = 1.5f;


    // Start is called before the first frame update
    private void Awake()
    {
        // Fade in animation of lore elements
        DOTween.Sequence()
          // Part 1
          .Append(lorePart1Img.DOFade(1f, imageAnimationTime))
          .Join(lorePart1Text.DOFade(1f, textAnimationTime))
          // Delay
          .Append(lorePart1Img.DOFade(1f, segmentDelay))
          // Part 2
          .Append(lorePart2Img.DOFade(1f, imageAnimationTime))
          .Join(lorePart2Text.DOFade(1f, textAnimationTime))
          // Delay
          .Append(lorePart1Img.DOFade(1f, segmentDelay))
          // Part 3
          .Append(lorePart3Img.DOFade(1f, imageAnimationTime))
          .Join(lorePart3Text.DOFade(1f, textAnimationTime))
          // Delay
          .Append(lorePart1Img.DOFade(1f, segmentDelay))
          // Start Button
          .Append(startButtonImg.DOFade(1f, imageAnimationTime))
          .Join(startButtonText.DOFade(1f, textAnimationTime))
          // Startup Delay
          .SetDelay(startupDelay)
          .Play();
    }
}
