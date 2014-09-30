//
//  StartAppBanner.h
//  Unity
//
//  Created by StartApp on 6/8/14.
//  Copyright (c) 2013 StartApp. All rights reserved.
//  SDK version 2.1.1


#import <Foundation/Foundation.h>
#import "STABannerSize.h"
#import "STABannerView.h"

@interface STAUnityBanner : NSObject <STABannerDelegagteProtocol>

-(void)addBannerWithSize:(STABannerSize) size autoOrigin:(STAAdOrigin) origin;
-(void)addBannerWithSize:(STABannerSize) size origin:(CGPoint) origin;

-(void)setBannerPosition:(STAAdOrigin) origin;
-(void)setBannerFixedPosition:(CGPoint) origin;
-(void)setBannerSize:(STABannerSize) size;
-(void)didRotateFromInterfaceOrientation:(int) orientation;


-(STABannerSize) getBannerSizeFromChar:(const char *)bannerSize;
-(STAAdOrigin) getBannerPositionFromChar:(const char *)bannerPosition;

@end
