//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;



//public class GoogleAds : MonoBehaviour
//{
//    private InterstitialAd interstitial;
//    public static GoogleAds Instance;
//    private BannerView bannerView;

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    public void Start()
//    {
        

//        //Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(initStatus => { });
//        RequestInterstitial();
//        RequestBanner();
//    }
    
    

//    public void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-4758979811168493/1085016267";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-5875384463485278/9777329852";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Initialize an InterstitialAd.
//        this.interstitial = new InterstitialAd(adUnitId);
        
//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        this.interstitial.LoadAd(request);
        
        
//        // Called when an ad request has successfully loaded.
//        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//        // Called when an ad request failed to load.
//        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//        // Called when an ad is shown.
//        this.interstitial.OnAdOpening += HandleOnAdOpened;
//        // Called when the ad is closed.
//        this.interstitial.OnAdClosed += HandleOnAdClosed;
//        // Called when the ad click caused the user to leave the application.
//        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
//    }
    
//    public void HandleOnAdLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//                            + args.Message);
//        print("Interstitial failed to load: " + args.Message);
//        // Handle the ad failed to load event.
//    }

//    public void HandleOnAdOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdOpened event received");
//    }

//    public void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdClosed event received");
//    }

//    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLeavingApplication event received");
//    }
   
//    public void CallAds()
//    {

//        if (this.interstitial.IsLoaded()) {
//            this.interstitial.Show();
//        }
//        RequestInterstitial();
//    }

//    public void RequestBanner()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-4758979811168493/2206526246";
//#elif UNITY_IPHONE
//            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
//#else
//            string adUnitId = "unexpected_platform";
//#endif

//        // Create a 320x50 banner at the top of the screen.
//        // AdSize adSize = new AdSize(320, 40);
//        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
//        AdRequest requestbanner = new AdRequest.Builder().Build();

//        // Load the banner with the request.
//        bannerView.LoadAd(requestbanner);

//    }




//}