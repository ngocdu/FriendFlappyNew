using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, 
		                                 transform.position.y, transform.position.z);
	}
}
