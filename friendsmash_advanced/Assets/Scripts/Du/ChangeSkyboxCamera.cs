using UnityEngine;
using System.Collections;

public class ChangeSkyboxCamera : MonoBehaviour {
	public Material [] materialSkys;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Skybox>().material = materialSkys[Random.Range(0, materialSkys.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
