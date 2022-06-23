using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class AnalyticsManager : MonoBehaviour
{
    string consentIdentifier;
    bool consentRequired;

    // Start is called before the first frame update
    async void Start()
    {
        InitializationOptions options = new InitializationOptions();
#if !UNITY_WEBGL
        options.SetEnvironmentName("testing");
#else
        options.SetEnvironmentName("midterm");
#endif
        await UnityServices.InitializeAsync(options);
        //List<string> consentIdentifiers = await Events.CheckForRequiredConsents();
        List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        if (consentIdentifiers.Count > 0)
        {
            consentIdentifier = consentIdentifiers[0];
            consentRequired = consentIdentifier == "pipl";
        }
        if (consentRequired)
        {
            //Events.ProvideOptInConsent(consentIdentifier, false);
            AnalyticsService.Instance.ProvideOptInConsent(consentIdentifier, false);
        }
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
