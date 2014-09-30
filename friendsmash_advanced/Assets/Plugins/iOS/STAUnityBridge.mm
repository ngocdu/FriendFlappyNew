//
//  startAppUnity.m
//  Unity
//
//  Created by StartApp on 6/8/14.
//  Copyright (c) 2013 StartApp. All rights reserved.
//  SDK version 2.1.1


#import "STAUnityBridge.h"

#import "STAUnityAd.h"
#import "STAUnityBanner.h"

#import "STAStartAppSDK.h"

@interface STAUnityBridge ()
{
}
@end

@implementation STAUnityBridge

static STAUnityAd *startAppAd = [[STAUnityAd alloc]init];
static STAUnityBanner *startAppBanner = [[STAUnityBanner alloc]init];

static const char* lastAdDelegate;
static STAAdType lastAdType;
static const char* bannerDelegate;




+(void)didLoadAd
{
    _didLoadAd();
}

+(void)failedLoadAdWithError:(NSString *)error
{
    _failedLoadAd([error UTF8String]);
}

+(void)didShowAd
{
    _didShowAd();
}

+(void)failedShowAdWithError:(NSString *)error
{
    _failedShowAd([error UTF8String]);
}

+(void)didCloseAd
{
    _didCloseAd();
    [startAppAd loadAd:lastAdType];

}







+ (void) didDisplayBannerAd{
    _didDisplayBannerAd();
}
+ (void) failedLoadBannerAdWithError:(NSString *)error{
    _failedLoadBannerAd([error UTF8String]);
}
+ (void) didClickBannerAd{
    _didClickBannerAd();
}



+ (void)sdkInitilizeWithAppId:(NSString*) appId withDevId:(NSString*)devId {
    STAStartAppSDK *sdk = [STAStartAppSDK sharedInstance];
    sdk.appID=appId;
    sdk.devID=devId;
}



extern "C" {
    
    //ad callbacks
    void _didLoadAd () {
        
        UnitySendMessage(lastAdDelegate, "didLoadAd", "");
    }
    
    void _failedLoadAd ( const char *error ) {
        
        UnitySendMessage(lastAdDelegate, "failedLoadAd", error);
    }
    
    void _didShowAd () {
        
        UnitySendMessage(lastAdDelegate, "didShowAd", "");
        UnitySendMessage("StartAppWrapperiOS", "lockScreen", "");
    }
    
    void _failedShowAd ( const char *error ) {
        
        UnitySendMessage(lastAdDelegate, "failedShowAd", error);
    }
    
    void _didCloseAd () {
        UnitySendMessage("StartAppWrapperiOS", "lockScreen", "");
        UnitySendMessage(lastAdDelegate, "didCloseAd", "");
    }
    
    
    
    //banner callbacks
    
    void _didDisplayBannerAd () {
        
        UnitySendMessage(bannerDelegate, "didDisplayBannerAd", "");
    }
    
    void _failedLoadBannerAd ( const char *error ) {
        
        UnitySendMessage(bannerDelegate, "failedLoadBannerAd", error);
    }
    
    void _didClickBannerAd () {
        
        UnitySendMessage(bannerDelegate, "didClickBannerAd", "");
    }
    
    
   
    
    
    //iunitialize function
    void _sdkInitilize (const char* appId, const char* devId)
    {
   		[STAUnityBridge sdkInitilizeWithAppId:[NSString stringWithUTF8String:appId] withDevId:[NSString stringWithUTF8String:devId]];
	}
    
    
    //ad functions
    void _loadAd (const char* adType, const char* objectDelegate)
    {
        char* res = (char*)malloc(strlen(objectDelegate) + 1);
        strcpy(res, objectDelegate);
        lastAdDelegate = res;
        lastAdType = [startAppAd getAdTypeFromChar:adType];
        
		[startAppAd loadAd:lastAdType];
	}
    
    
    void _showAd ()
	{
		[startAppAd showAd];
	}
    
    //banner functions
    void _addBanner (const char* bannerType,const char* bannerPosition,const char* bannerSize, const char* objectDelegate)
	{
        char* res = (char*)malloc(strlen(objectDelegate) + 1);
        strcpy(res, objectDelegate);
        bannerDelegate = res;
        
        [startAppBanner addBannerWithSize:[startAppBanner getBannerSizeFromChar:bannerSize] autoOrigin:[startAppBanner getBannerPositionFromChar:bannerPosition]];
        
    }
    
    void _addBannerAtFixedOrigin (const char* bannerType,int x,int y,const char* bannerSize, const char* objectDelegate)
	{
        char* res = (char*)malloc(strlen(objectDelegate) + 1);
        strcpy(res, objectDelegate);
        bannerDelegate = res;
        
        [startAppBanner addBannerWithSize:[startAppBanner getBannerSizeFromChar:bannerSize] origin:CGPointMake(x,y)];
    }
    
    void _setBannerPosition(const char* bannerPosition)
    {
        [startAppBanner setBannerPosition:[startAppBanner getBannerPositionFromChar:bannerPosition]];
    }
    
	void _setBannerFixedPosition(int x,int y)
    {
        [startAppBanner setBannerFixedPosition:CGPointMake(x,y)];
    }
	void _setBannerSize(const char* bannerSize)
    {
        [startAppBanner setBannerSize:[startAppBanner getBannerSizeFromChar:bannerSize]];
    }
    
    void _didRotateFromInterfaceOrientation(int orientation)
    {
        [startAppBanner didRotateFromInterfaceOrientation:orientation];
    }
    
    //Others functions
    
    bool _STAShouldAutoRotate()
    {
        return [startAppAd shouldAutoRotate];
    }
}

@end
