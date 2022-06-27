using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    //string consentIdentifier;
    //bool consentRequired;

    // Start is called before the first frame update
    async void Start()
    {
        InitializationOptions options = new();
#if UNITY_EDITOR
        options.SetEnvironmentName("testing");
#else
        options.SetEnvironmentName("midterm");
#endif
        await UnityServices.InitializeAsync(options);
        Debug.Log("LMAO ANAL INIT");
    }

    public void HandleEvent(string eventName, IDictionary<string, object> eventParams)
    {
        Debug.LogFormat("EVENT HANDLED: {0}", Analytics.CustomEvent(eventName, eventParams));
        Debug.LogFormat("EVENT FLUSHED: {0}", Analytics.FlushEvents());
        //AnalyticsService.Instance.CustomData(eventName, eventParams);
        //AnalyticsService.Instance.Flush();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
