using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float speed;
	public Vector3 force;
	public float maxX;
	// Use this for initialization
	void Start () {
		rigidbody.AddForce(force);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x >= maxX)
			Destroy(gameObject);
	}
}
