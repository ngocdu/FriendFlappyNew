using UnityEngine;
using System.Collections;
using StartApp;
public class ShowStartapp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartAppWrapperiOS.loadAd();
		if(Random.Range(0, 3) == 1)
		StartAppWrapperiOS.showAd();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
