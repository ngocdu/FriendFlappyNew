//
//  StartAppObject.h
//  Unity
//
//  Created by StartApp on 6/8/14.
//  Copyright (c) 2013 StartApp. All rights reserved.
//  SDK version 2.1.1


#import <Foundation/Foundation.h>
#import "STAStartAppAd.h"

@interface STAUnityAd : NSObject <STADelegateProtocol>
{
    
}

-(void)loadAd:(STAAdType) adType;
-(void)showAd;

-(BOOL)shouldAutoRotate;


-(STAAdType) getAdTypeFromChar:(const char *)adType;

@end
