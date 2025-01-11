using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string header;
    [SerializeField, TextArea(5, 10)] string content;

    Coroutine showTooltipCoroutine;
    public void OnPointerEnter(PointerEventData eventData)
    {
        showTooltipCoroutine = StartCoroutine(ShowTooltipAfterDelay(0.25f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (showTooltipCoroutine != null)
        {
            StopCoroutine(showTooltipCoroutine);
            showTooltipCoroutine = null;
        }
        TooltipSystem.Hide();
    }

    private IEnumerator ShowTooltipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TooltipSystem.Show(content, header);
    }

    public void SetContent(string header, string content)
    {
        this.content = content;
        this.header = header;
    }

}
