using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    public Image activeMicrophone;
    public bool debugPhrases = true;
    public Transform phraseParent;
    public VoicePhraseView phrasePrefab;

    public List<VoiceCommand> voiceCommands;

    private bool voiceEnabled = false;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> commandsDictionary = new Dictionary<string, Action>();
    private Coroutine disableVoiceCoroutine;

    float delay = 1.25f;

    private void Start()
    {
        activeMicrophone.gameObject.SetActive(false);
    }

    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        if (voiceEnabled)
        {
            if (debugPhrases) PrintPhrase(speech);

            if (commandsDictionary.TryGetValue(speech.text, out Action action))
            {
                action?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (disableVoiceCoroutine != null)
            {
                StopCoroutine(disableVoiceCoroutine);
                disableVoiceCoroutine = null;
            }
            if (!voiceEnabled)
            {
                voiceEnabled = true;
                activeMicrophone.gameObject.SetActive(true);
            }
        }
        else if (voiceEnabled && disableVoiceCoroutine == null)
        {
            disableVoiceCoroutine = StartCoroutine(DisableVoiceAfterDelay());
        }

        ;
    }

    private IEnumerator DisableVoiceAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        voiceEnabled = false;
        activeMicrophone.gameObject.SetActive(false);
        disableVoiceCoroutine = null;
    }

    private static void PrintPhrase(PhraseRecognizedEventArgs speech)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", speech.text, speech.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", speech.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", speech.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
    }

    void Awake()
    {
        AssignCommands();

        keywordRecognizer = new KeywordRecognizer(commandsDictionary.Keys.ToArray(), ConfidenceLevel.Rejected);
        keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
        keywordRecognizer.Start();
    }

    private void AssignCommands()
    {
        foreach (var command in voiceCommands)
        {
            var phraseUI = Instantiate(phrasePrefab, phraseParent);
            phraseUI.Init(command);

            foreach (var phrase in command.VoicePhrases)
            {
                commandsDictionary.Add(phrase, command.Execute);
            }
        }
    }

    private void OnDisable()
    {
        keywordRecognizer.OnPhraseRecognized -= OnVoiceRecognized;
        keywordRecognizer.Stop();
    }
}
