using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed = 2.0f;

	void FixedUpdate () {
		transform.position += Vector3.left *speed* Time.deltaTime;
	}
}
