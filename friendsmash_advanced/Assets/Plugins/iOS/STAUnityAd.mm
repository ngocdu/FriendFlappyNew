//
//  StartAppObject.m
//  Unity
//
//  Created by StartApp on 6/8/14.
//  Copyright (c) 2013 StartApp. All rights reserved.
//  SDK version 2.1.1


#import "STAStartAppSDK.h"

#import "STAUnityAd.h"
#import "STAUnityBridge.h"

@interface STAUnityAd ()
{
    STAStartAppAd *startAppAd;
}
@end

@implementation STAUnityAd

- (id)init {
    self = [super init];
    if (self) {
        startAppAd = [[STAStartAppAd alloc]init];
    }
    return self;
}


-(void)loadAd:(STAAdType) adType {
    [startAppAd loadAd:adType withDelegate:self];
}

-(void)showAd{
    [startAppAd showAd];
}


-(BOOL)shouldAutoRotate{
    return startAppAd.STAShouldAutoRotate;
}


-(void)didLoadAd:(STAAbstractAd *)ad{
    [STAUnityBridge didLoadAd];
}
- (void) failedLoadAd:(STAAbstractAd*)ad withError:(NSError *)error{
    [STAUnityBridge failedLoadAdWithError:[error localizedDescription]];
}
- (void) didShowAd:(STAAbstractAd*)ad{
    [STAUnityBridge didShowAd];
}
- (void) failedShowAd:(STAAbstractAd*)ad withError:(NSError *)error{
    [STAUnityBridge failedShowAdWithError:[error localizedDescription]];
}
- (void) didCloseAd:(STAAbstractAd*)ad{
    [STAUnityBridge didCloseAd];
}


-(STAAdType) getAdTypeFromChar:(const char *)adType{
    STAAdType type = STAAdType_FullScreen;
    if([[NSString stringWithUTF8String:adType] isEqual:@"STAAdType_FullScreen"])
    {
        type=STAAdType_FullScreen;
    }else if ([[NSString stringWithUTF8String:adType] isEqual:@"STAAdType_OfferWall"]) {
        type=STAAdType_OfferWall;
    }else if ([[NSString stringWithUTF8String:adType] isEqual:@"STAAdType_Automatic"]) {
        type=STAAdType_Automatic;
    }else if ([[NSString stringWithUTF8String:adType] isEqual:@"STAAdType_AppWall"]) {
        type=STAAdType_AppWall;
    }else  if ([[NSString stringWithUTF8String:adType] isEqual:@"STAAdType_Overlay"]) {
        type=STAAdType_Overlay;
    }
    return type;
}


- (void) dealloc {
    [super dealloc];
}


@end