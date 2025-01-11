using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextManager : Singleton<InfoTextManager>
{
    [SerializeField] Transform infoParent;
    [SerializeField] InfoText infoText;
    Queue<InfoText> infoQueue = new Queue<InfoText>();
    private bool isDissolving = false;
    public void AddInformation(string text, InfoLenght lenght = InfoLenght.Medium)
    {
        //var info = Instantiate(infoText, infoParent);
        //info.Init(text, lenght);
        //infoQueue.Enqueue(info);
        //if (!isDissolving)
        //{
        //    StartCoroutine(ProcessQueue());
        //}
    }

    IEnumerator ProcessQueue()
    {
        isDissolving = true;

        while (infoQueue.Count > 0)
        {
            InfoText currentInfo = infoQueue.Dequeue();
            yield return StartCoroutine(currentInfo.Dissolver());
        }

        isDissolving = false;
    }

}
