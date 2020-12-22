using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public event Action onAdFinished = delegate { };

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3944143", true);
    }

    public void ShowAdd(string placementID)
    {
        StartCoroutine(ShowAnAd(placementID));
    }

    private IEnumerator ShowAnAd(string placementID)
    {
        while (!Advertisement.IsReady(placementID))
        {
            yield return null;
        }

        Advertisement.Show(placementID);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            LootBoxAmount.SetLootBoxAmount(1);
            onAdFinished();
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }
}
