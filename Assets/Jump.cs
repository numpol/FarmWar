using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private Rigidbody2D rigidbody2D;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		//transform.position += Vector3.left *speed* Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//	coll.gameObject.SendMessage("ApplyDamage", 10);

		//if(col.gameObject.name == "prop_powerCube")
		{
			Debug.Log ("OnCollisionEnter");
			rigidbody2D.AddForce(new Vector2(-100,300));
		}
	}
}
