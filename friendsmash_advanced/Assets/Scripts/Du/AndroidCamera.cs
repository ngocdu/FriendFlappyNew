using UnityEngine;
using System.Collections;
using System.IO;

public class AndroidCamera : MonoBehaviour
{
//	public Vector2 photoSize;
//	public int photoFrames;
//	public GUIStyle guiStyle;
//	
//	private WebCamTexture webCamTexture;
//	private string camName;
//	
//	void Start () 
//	{
////		WebCamDevice[] devices = WebCamTexture.devices;
////		webCamTexture = new WebCamTexture(GetCamera(), (int)photoSize.x, (int)photoSize.y, photoFrames);
////		renderer.material.mainTexture = webCamTexture;
////		webCamTexture.Play();
////		Screen.orientation = ScreenOrientation.Portrait;
//	}
//	
////	void TakePhoto()
////	{   
////		Texture2D takenPhoto = new Texture2D((int)photoSize.x, (int)photoSize.y, TextureFormat.ARGB32, false);
////		
////		Color[] texData = webCamTexture.GetPixels();
////		
////		takenPhoto.SetPixels(texData);
////		takenPhoto.Apply();
////		
////		byte[] photoData = takenPhoto.EncodeToPNG();
////		Destroy(takenPhoto);
////		
////		//if(File.Exists(Application.persistentDataPath + "/AvatarPhoto.png"))
////		//{
////		//  File.Delete(Application.persistentDataPath + "/AvatarPhoto.png");
////		//}
////		File.WriteAllBytes(Application.dataPath + "/AvatarPhoto.png", photoData);
////	}
//	IEnumerator TakePhoto()
//	{   
//		webCamTexture.Pause();
//		yield return new WaitForSeconds(1.0f);
//		
//		Texture2D takenPhoto = new Texture2D((int)photoSize.x, (int)photoSize.y, TextureFormat.ARGB32, false);
//		yield return new WaitForSeconds(1.0f);
//		
//		Color[] texData = webCamTexture.GetPixels();
//		yield return new WaitForSeconds(1.0f);
//		
//		takenPhoto.SetPixels(texData);
//		yield return new WaitForSeconds(1.0f);
//		
//		takenPhoto.Apply();
//		yield return new WaitForSeconds(1.0f);
//		
//		byte[] photoData = takenPhoto.EncodeToPNG();
//		yield return new WaitForSeconds(1.0f);
//		
//		Destroy(takenPhoto);
//		yield return new WaitForSeconds(2.0f);
//		
//		File.WriteAllBytes(Application.dataPath + "/AvatarPhoto.png", photoData);
//		yield return new WaitForSeconds(2.0f);
//		
//		Debug.Log("Done");
//		yield return 0;
//	}
//	
//	void OnGUI()
//	{
//		if(GUI.Button(new Rect(0, 10, Screen.width, Screen.height / 5), "TAKE PHOTO!"))
//		{
//			TakePhoto();
//		}
//	}
}