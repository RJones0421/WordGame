using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class AnalyticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        InitializationOptions options = new InitializationOptions();
        options.SetEnvironmentName("testing");
        await UnityServices.InitializeAsync(options);
        Debug.Log("LMAO ANAL INIT");
    }

    public void HandleEvent(string eventName, IDictionary<string, object> eventParams)
    {
        Debug.Log("EVENT HANDLED");
        AnalyticsService.Instance.CustomData(eventName, eventParams);
        AnalyticsService.Instance.Flush();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
