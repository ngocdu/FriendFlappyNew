using UnityEngine;
using System.Collections;

using System;
using System.Runtime.InteropServices;


public class StartAppWrapperiOS : MonoBehaviour {
	
	private static string developerId;
	private static string applicatonId;
	private static Boolean initilized = false;
	
	private static bool autorotateToPortrait = Screen.autorotateToPortrait;
	private static bool autorotateToPortraitUpsideDown = Screen.autorotateToPortraitUpsideDown;
	private static bool autorotateToLandscapeLeft = Screen.autorotateToLandscapeLeft;
	private static bool autorotateToLandscapeRight = Screen.autorotateToLandscapeRight;


	public enum AdType{
		STAAdType_Automatic,
		STAAdType_FullScreen,
		STAAdType_AppWall,
		STAAdType_Overlay
	};

	
	public enum BannerType{
		AUTOMATIC
	};

	public enum BannerPosition{
		BOTTOM,
		TOP
	};
	
	public enum BannerSize{
		STA_AutoAdSize,
		STA_PortraitAdSize_320x50,
		STA_LandscapeAdSize_480x50,
		STA_PortraitAdSize_768x90,
		STA_LandscapeAdSize_1024x90
	};

	public class BannerFixedPosition {
		public int x;
		public int y;
	}

	public class STABannerProperties {
		public BannerType type; 
		public BannerPosition position;
		public BannerFixedPosition fixedPosition = new BannerFixedPosition();
		public bool useFixedPosition;
		public BannerSize size;
		public string delegateName;
	}

	public class STAInterstitialProperties {
		public AdType type; 
		public string delegateName;
	}






	private static string getStringFromBannerSize(BannerSize bannerSize)
	{
		string size = @"STA_AutoAdSize";
		if(bannerSize==BannerSize.STA_PortraitAdSize_320x50){
			size = @"STA_PortraitAdSize_320x50";
		}else if (bannerSize==BannerSize.STA_LandscapeAdSize_480x50) {
			size = @"STA_LandscapeAdSize_480x50";
		}else if (bannerSize==BannerSize.STA_PortraitAdSize_768x90) {
			size = @"STA_PortraitAdSize_768x90";
		}else if (bannerSize==BannerSize.STA_LandscapeAdSize_1024x90) {
			size = @"STA_LandscapeAdSize_1024x90";
		}else if (bannerSize==BannerSize.STA_AutoAdSize) {
			size = @"STA_AutoAdSize";
		}
		return size;
	}

	private static string getStringFromBannerPosition(BannerPosition bannerPosition)
	{
		string position = @"BOTTOM";
		if(bannerPosition==BannerPosition.TOP){
			position = @"TOP";
		}else {
			position = @"BOTTOM";
		}
		return position;
	}
	
	
	private static string getStringFromAdType(AdType adType)
	{
		string type = @"STAAdType_Automatic";
		if(adType==AdType.STAAdType_FullScreen){
			type = @"STAAdType_FullScreen";
		}else if (adType==AdType.STAAdType_Automatic) {
			type = @"STAAdType_Automatic";
		}else if (adType==AdType.STAAdType_AppWall) {
			type = @"STAAdType_OfferWall";
		}else if (adType==AdType.STAAdType_Overlay) {
			type = @"STAAdType_Overlay";
		}
		return type;
	}

	private static int getIntFromDeviceOrientation(DeviceOrientation orientation)
	{
		int orien = 0;
	    if (orientation == DeviceOrientation.Portrait) {
			orien = 0;
		}else if (orientation == DeviceOrientation.PortraitUpsideDown) {
			orien = 1;
		}else if (orientation == DeviceOrientation.LandscapeLeft) {	
			orien = 2;
		}else if (orientation == DeviceOrientation.LandscapeRight) {
			orien = 3;
		}
		return orien;
	}



	//sdk initilize functions
	private static void checkInitilize()
	{
		if (initilized)
			return;
		if (!initUserData ()) {
			throw new System.ArgumentException ("Error in initializing Application ID or Developer ID, Verify you initialized them in StartAppDataiOS in resources");
		} else {
			_sdkInitilize (applicatonId, developerId);
			initilized = true;
		}
	}
	
	private static void sdkInitilize(string appId,string devId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			_sdkInitilize(appId,devId);
		}
	}
	
	private static bool initUserData(){
		bool result = false;
		int assigned = 0;
		
		TextAsset data = (TextAsset)Resources.Load("StartAppDataiOS");
		string userData = data.ToString();
		
		string[] splitData = userData.Split('\n');
		string[] singleData;
		
		for (int i = 0; i < splitData.Length; i++){
			singleData = splitData[i].Split('=');
			if (singleData[0].ToLower().CompareTo("applicationid") == 0){
				assigned++;
				applicatonId = singleData[1].ToString().Trim();
			}
			
			if (singleData[0].ToLower().CompareTo("developerid") == 0){
				assigned++;
				developerId = singleData[1].ToString().Trim();
			}
		}
		
		if (assigned == 2){
			result = true;
		}
		return result;
	}





	//sdk initilize

	[DllImport ("__Internal")]
	private static extern void _sdkInitilize(string appId, string devId);

	// ad 
	[DllImport ("__Internal")]
	private static extern void _loadAd(string adType, string objectDelegate);

	[DllImport ("__Internal")]
	private static extern void _showAd();
	
	// banner
	[DllImport ("__Internal")]
	private static extern void _addBanner(string bannerType,string bannerPosition,string bannerSize,string objectDelegate);

	[DllImport ("__Internal")]
	private static extern void _addBannerAtFixedOrigin(string bannerType,int x,int y,string bannerSize,string objectDelegate);

	[DllImport ("__Internal")]
	private static extern void _setBannerPosition(string bannerPosition);

	[DllImport ("__Internal")]
	private static extern void _setBannerFixedPosition(int x,int y);

	[DllImport ("__Internal")]
	private static extern void _setBannerSize(string bannerSize);

	[DllImport ("__Internal")]
	private static extern void _didRotateFromInterfaceOrientation(int orientation);

	[DllImport ("__Internal")]
	private static extern bool _STAShouldAutoRotate();


	//ad functions
	public static void loadAd(STAInterstitialProperties adProp)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			if (adProp.delegateName == null) {
				adProp.delegateName = "";
			}
			_loadAd(getStringFromAdType(adProp.type),adProp.delegateName);
		}
	}
	public static void loadAd(AdType adType,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd(getStringFromAdType(adType),objectDelegate);
		}
	}
	public static void loadAd(AdType adType)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd(getStringFromAdType(adType),"");
		}
	}
	public static void loadAd(string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd("STAAdType_Automatic",objectDelegate);
		}
	}
	public static void loadAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd("STAAdType_Automatic","");
		}
	}

	public static void showAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			_showAd();
	}


	//banner fuctions
	//Full/4 objects
	public static void addBanner(STABannerProperties bannerProperties)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			if (bannerProperties.delegateName == null) {
				bannerProperties.delegateName = "";
			}
			if(bannerProperties.useFixedPosition == true)
			{
				_addBannerAtFixedOrigin("AUTOMATIC",bannerProperties.fixedPosition.x,bannerProperties.fixedPosition.y,getStringFromBannerSize(bannerProperties.size),bannerProperties.delegateName);
			}else {
				_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerProperties.position),getStringFromBannerSize(bannerProperties.size),bannerProperties.delegateName);
			}
		}
	}
	public static void addBanner(BannerType bannerType,BannerPosition bannerPosition,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	public static void addBanner(BannerType bannerType,int x,int y,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}

	//3 objects
	public static void addBanner(BannerPosition bannerPosition,BannerSize bannerSize,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	public static void addBanner(int x,int y,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}

	//2 objects
	public static void addBanner(BannerPosition bannerPosition,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),"STA_AutoAdSize",objectDelegate);
		}
	}
	public static void addBanner(int x,int y,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,"STA_AutoAdSize",objectDelegate);
		}
	}

	public static void addBanner(BannerSize bannerSize,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM",getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}

	public static void addBanner(BannerPosition bannerPosition,BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),"");
		}
	}
	public static void addBanner(int x,int y,BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),"");
		}
	}



	//1 objects
	public static void addBanner(string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM","STA_AutoAdSize",objectDelegate);
		}
	}
	public static void addBanner(BannerPosition bannerPosition)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),"STA_AutoAdSize","");
		}
	}
	public static void addBanner(int x,int y)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,"STA_AutoAdSize","");
		}
	}
	public static void addBanner(BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM",getStringFromBannerSize(bannerSize),"");
		}
	}
	//0 objects
	public static void addBanner()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM","STA_AutoAdSize","");
		}
	}

	public static void setBannerPosition(BannerPosition bannerPosition)
	{
		_setBannerPosition(getStringFromBannerPosition(bannerPosition));
	}
	public static void setBannerPosition(int x,int y)
	{
		_setBannerFixedPosition(x,y);
	}
	public static void setBannerSize(BannerSize bannerSize)
	{
		_setBannerSize(getStringFromBannerSize(bannerSize));
	}

	public static void didRotateFromInterfaceOrientation(DeviceOrientation orientation)
	{
		if (_STAShouldAutoRotate()) {
			_didRotateFromInterfaceOrientation(getIntFromDeviceOrientation(orientation));
		}
	}

	//others function
	public static bool STAShouldAutoRotate()
	{
		return _STAShouldAutoRotate();
	}

	public static void lockScreen()
	{
		Screen.autorotateToPortrait = autorotateToPortrait && StartAppWrapperiOS.STAShouldAutoRotate();
		Screen.autorotateToPortraitUpsideDown = autorotateToPortraitUpsideDown && StartAppWrapperiOS.STAShouldAutoRotate();
		Screen.autorotateToLandscapeLeft = autorotateToLandscapeLeft && StartAppWrapperiOS.STAShouldAutoRotate();
		Screen.autorotateToLandscapeRight = autorotateToLandscapeRight && StartAppWrapperiOS.STAShouldAutoRotate();
	}
	
}