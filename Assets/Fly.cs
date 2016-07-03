using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

	public float speed = 2.0f;

	private GameObject player;
	private Rigidbody2D rigidbody2D;

	void Start () {
		player = GameObject.Find("Player");
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {

		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

		if (Vector3.Distance(transform.position,player.transform.position)>1f){//move if distance from target is greater than 1
			transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
		}
	}

}
