using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Analytics;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    private static int deaths;

    [SerializeField]
    private static Dictionary<string, string> BASE_URLS;

    [SerializeField]
    private static Dictionary<string, List<string>> FORM_FIELDS;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        deaths = 0;

#if UNITY_EDITOR
        BASE_URLS = new Dictionary<string, string>
        {
            { "death", "https://docs.google.com/forms/u/0/d/e/1FAIpQLScfdwzslCWGBiR-SCgcWf-4VoMapN0i_DwyCYcD2yoAChRchA/formResponse" },
            { "wordSubmitted", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc7TcmmhSbkRN_hwuMe4Wt2Qs3AbFpIDhiVQ0EEyfvhIWe8pw/formResponse" }
        };
#else
        BASE_URLS = new Dictionary<string, string>
        {
            { "death", "https://docs.google.com/forms/u/0/d/e/1FAIpQLScU1F9e2aNVNkqZ5_P8dRb_VNswbuf1OklGGwnEGlN4NHRcgw/formResponse" },
            { "wordSubmitted", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdUxWvh2_BVb50f6CK1kJt-i75p6dM3iXyWt9cyS4CpuuRlDQ/formResponse" }
        };
#endif

        FORM_FIELDS = new Dictionary<string, List<string>>
        {
            { "death", new List<string>
            {
                "entry.452223131",  // sessionID
                "entry.2099981536", // deaths
                "entry.663547218",  // cause
                "entry.1803891916", // time
                "entry.1260511242", // userScore
                "entry.681068843",  // validWordCount
                "entry.1556221476", // totalSubmissions
                "entry.433458864",  // totalWordLength
                "entry.223216808"   // totalValidWordLength
            }
            },
            { "wordSubmitted", new List<string>
            {
                "entry.1318871643", // sessionID
                "entry.1534152862", // deaths
                "entry.404780885",  // time
                "entry.275868668",  // validWord
                "entry.268931933",  // word
                "entry.268931933",  // wordLength
                "entry.1976654319"  // wordScore
            }
            }
        };
    }

    void Awake()
    {
        deaths++;
    }

    IEnumerator Post(string eventName, List<object> eventParams)
    {
        WWWForm form = new();
        List<string> fieldNames = FORM_FIELDS[eventName];
        for (int i = 0; i < fieldNames.Count; i++)
        {
            Debug.Log(fieldNames[i]);
        }
        form.AddField(fieldNames[0], AnalyticsSessionInfo.sessionId.ToString());
        form.AddField(fieldNames[1], deaths);
        for (int i = 2; i < fieldNames.Count; i++)
        {
            form.AddField(fieldNames[i], eventParams[i - 2].ToString());
        }

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URLS[eventName], rawData);
        yield return www;

        /*
        UnityWebRequest www = UnityWebRequest.Post(BASE_URLS[eventName], form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.LogFormat("Form upload complete! {0}", eventName);
        }
        */
    }

    public void HandleEvent(string eventName, List<object> eventParams)
    {
        Debug.Log("EVENT HANDLED");
        StartCoroutine(Post(eventName, eventParams));
    }

    public void HandleEvent(string eventName, IDictionary<string, object> eventParams)
    {
        Debug.LogFormat("EVENT HANDLED: {0}", Analytics.CustomEvent(eventName, eventParams));
        Debug.LogFormat("EVENT FLUSHED: {0}", Analytics.FlushEvents());
    }
}
