using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
	public GameObject bomb;
	public Transform bombSpawn;
	public Vector2 bombStartForce = new Vector2(100,200);
	public float speed;
	public float tilt;
	public Boundary boundary;

	public Vector3 originalPosition;
	public Quaternion originalRotationValue; // declare this as a Quaternion
	float rotationResetSpeed = 10.0f;

	void Start () {
		originalRotationValue = transform.rotation; // save the initial rotation
		originalPosition = transform.position;
	}

	public void DropBomb (){
		Debug.Log ("DropBomb");
		GameObject bombObject = Instantiate(bomb, bombSpawn.position, bombSpawn.rotation) as GameObject;
		Rigidbody2D bombRigid2D = bombObject.GetComponent<Rigidbody2D> ();
		bombRigid2D.AddForce (bombStartForce);
	}

	public void UseBasket (){
		Debug.Log ("Basket");
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical );
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
			
		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector2 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
			);

		//rotate back
		if (movement.magnitude == 0) {

			Vector3 vectorToTarget = originalPosition - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);


			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed); 
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Destroy (coll.gameObject);
		}
	}
}