//
//  startAppUnity.h
//  Unity
//
//  Created by StartApp on 6/8/14.
//  Copyright (c) 2013 StartApp. All rights reserved.
//  SDK version 2.1.1


#import <UIKit/UIKit.h>
#import "STAStartAppAd.h"

@interface STAUnityBridge : NSObject

+ (void)sdkInitilizeWithAppId:(NSString*) appId withDevId:(NSString*)devId;

+(void)didLoadAd;
+(void)failedLoadAdWithError:(NSString *)error;
+(void)didShowAd;
+(void)failedShowAdWithError:(NSString *)error;
+(void)didCloseAd;

+ (void) didDisplayBannerAd;
+ (void) failedLoadBannerAdWithError:(NSString *)error;
+ (void) didClickBannerAd;

@end
