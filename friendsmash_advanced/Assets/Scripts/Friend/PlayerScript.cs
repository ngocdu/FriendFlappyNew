using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Vector3 forceTouch ;
	// Use this for initialization
	public Texture FriendTexture;
	public GameObject [] bulletPrefabs;
	void Start () {
		renderer.material.mainTexture = GameStateManager.UserTexture;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) == true)
		{
			Debug.Log("daaaaaaaaaaaaaaaaa");
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce(forceTouch);

			GameObject bullet = (GameObject)Instantiate(
				bulletPrefabs[Random.Range(0, bulletPrefabs.Length)],
				transform.position,
				Quaternion.identity
				);
			bullet.transform.Rotate(new Vector3(0,90,0));
		}
	}
}
