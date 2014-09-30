using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{

    public float YMin = -120.0f;
	public float XMin = -175.0f;
	public Transform []praticlePrefabs;
//	private Ray ray;
    // Update is called once per frame
    void Update()
    {
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		RaycastHit hit;
//		if (Physics.Raycast(ray, out hit))
//		{
//			if(Input.GetMouseButtonDown(0))
//			{
////				Debug.DrawLine(ray.origin, hit.point);
////				Debug.Log(hit.collider.gameObject.name);
//				if (hit.collider.gameObject.tag == "Friend")
//				{
//					var praticleTranform = Instantiate(praticlePrefabs[Random.Range(0, praticlePrefabs.Length)]) as Transform;
////					praticleTranform.position = new Vector3(this.transform.position.x, 
////					                                        this.transform.position.y, -160);
//					praticleTranform.position = this.transform.position;
////					praticleTranform.localScale = new Vector3(9,9,9);
//					Destroy(praticleTranform.gameObject, 2);
//					GameStateManager.onFriendSmash();
//					SoundEffectsHelper.Instance.MakeExplosionSound();
//					Destroy(hit.collider.gameObject);
//
//				}
//				else GameStateManager.onEnemySmash(hit.collider.gameObject);
//			}
//		}
//        if (transform.position.y <= YMin)
//        {
//            if (gameObject.tag == "Friend" && !GameStateManager.ScoringLockout) GameStateManager.onFriendDie();
//            Destroy(gameObject);
//        }
		if (transform.position.x <= XMin)
        {
//            if (gameObject.tag == "Friend" && !GameStateManager.ScoringLockout) GameStateManager.onFriendDie();
//			GameStateManager.onEnemySmash(gameObject);
			GameStateManager.onFriendDie();
            Destroy(gameObject);
        }
    }
//	void Update () {
//		
//		RaycastHit hit = new RaycastHit();
//		for (int i = 0; i < Input.touchCount; ++i) {
//			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
//				// Construct a ray from the current touch coordinates
//				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
//				if (Physics.Raycast(ray, out hit)) {
//					hit.transform.gameObject.SendMessage("OnMouseDown");
//				}
//			}
//		}
//	}
//    void OnMouseDown()
//    {
//        if (gameObject.tag == "Friend")
//        {
//            GameStateManager.onFriendSmash();
//            Destroy(gameObject);
//        }
//        else GameStateManager.onEnemySmash(gameObject);
//    }
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Bullet")
		{
			var praticleTranform = Instantiate(praticlePrefabs[Random.Range(0, praticlePrefabs.Length)]) as Transform;
			praticleTranform.position = new Vector3(this.transform.position.x, 
			                                        this.transform.position.y, -160);
			praticleTranform.position = this.transform.position;
			praticleTranform.localScale = new Vector3(9,9,9);
			Destroy(praticleTranform.gameObject, 10);
			GameStateManager.onFriendSmash();
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}
}
