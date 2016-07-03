using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float radius = 15.0F;
	public float power = 100.0F;

	public float ThetaScale = 0.01f;
	private int Size;
	private LineRenderer LineDrawer;
	private float Theta = 0f;

	void Start() {
		LineDrawer = GetComponent<LineRenderer>();
		StartCoroutine(ChargeBomb(3,0.1f));
	}

	void Update ()
	{      
		//TODO draw radius line
		Theta = 0f;
		Size = (int)((1f / ThetaScale) + 1f);
		LineDrawer.SetVertexCount(Size); 
		for(int i = 0; i < Size; i++){          
			Theta += (2.0f * Mathf.PI * ThetaScale);         
			float x = radius * Mathf.Cos(Theta);
			float y = radius * Mathf.Sin(Theta);          
			LineDrawer.SetPosition(i, new Vector3(x, y, 0));
		}
	}

	private IEnumerator ChargeBomb(float delayTime,float bombTime)
	{
		yield return new WaitForSeconds(delayTime);

		// Percentage of the scale we have completed.
		var t      = 0.0f;

		// The scale we're going from and the scale we're going to.
		var start   = transform.localScale;
		var end = transform.localScale * 5;

		// In other words while our scale is not equal to our "end" vector.
		while( t < 1.0f )
		{
			// Scale the bomb. 2.0 here because you specified the scale to be over 2 seconds.
			t += ( Time.deltaTime / bombTime );
			transform.localScale = Vector3.Lerp( start, end, t );

			// Wait till the next frame.
			yield return null;
		}

		// Bomb is scaled, invoke an event here.
		Debug.Log("Bomb!");
		Vector2 explosionPos = transform.position;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
		foreach (Collider2D hit in colliders) {
			Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

			if (rb != null) {
				rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
				if (rb.gameObject.tag == "Enemy") {
					Debug.Log("damaged");
				}
			}
		}

		Destroy (gameObject);
	}
}
