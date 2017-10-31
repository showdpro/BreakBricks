using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

    public static AdManager Instance {set; get;}
    public static BannerView bannerAd;

    private string bannerId = "ca-app-pub-4544102937912717/5993374934";
    private string videoId = " ca-app-pub-4544102937912717/9409219222";
  
    void Start () {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerAd = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(request);

        bannerAd.Show();
    }

  
}
