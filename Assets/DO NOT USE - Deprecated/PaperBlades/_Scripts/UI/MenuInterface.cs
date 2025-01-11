using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public static class VectorExtensions
{
    public static Vector3 With(this Vector3 v1, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? v1.x, y ?? v1.y, z ?? v1.z);
    }
    public static Vector3 Add(this Vector3 v1, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(v1.x + (x ?? 0), v1.y + (y ?? 0), v1.z + (z ?? 0));
    }
}


public static class ListExtensions
{
    public static List<T> SelectRandomElements<T>(this List<T> list, int count)
    {
        List<T> copiedList = new List<T>(list);
        List<T> selectedElements = new List<T>();

        if (selectedElements.Count > count)
        {
            Debug.LogWarning("Number of selected elements is greater than given list!");
            return copiedList;
        }

        while (selectedElements.Count < count)
        {
            int randomIndex = UnityEngine.Random.Range(0, copiedList.Count);
            selectedElements.Add(copiedList[randomIndex]);
            copiedList.RemoveAt(randomIndex);
        }

        return selectedElements;
    }

    public static T SelectRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("The list is empty or null.");
        }

        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}

public class MenuInterface : MonoBehaviour
{
    [SerializeField] Button startBtn = null;
    [SerializeField] Button quitBtn = null;
    [SerializeField] Button creditsBtn = null;

    [SerializeField] SoundOld buttonHover = null;
    [SerializeField] SoundOld buttonPressed = null;
    [SerializeField] SoundOld menuAmbient = null;

    private void Awake()
    {
        startBtn.onClick.AddListener(() => StartButtonOnClick());
        creditsBtn.onClick.AddListener(() => CreditsButtonOnClick());
        quitBtn.onClick.AddListener(() => QuitButtonOnClick());
    }

    public void Start()
    {
        AudioManagerOld.Instance.PlayGlobal(menuAmbient, SoundType.Music);
    }

    void StartButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.LoreIntro);
    }
    void CreditsButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.Credits);
    }
    void QuitButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        Application.Quit();
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }



}
