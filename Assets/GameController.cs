using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject[] monsters;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnMonster());
	}
	
	private IEnumerator SpawnMonster()
	{
		yield return new WaitForSeconds (3.0f);

		while (true) {

			Debug.Log ("SpawnMonster");

			int monsterType = Random.Range (0, monsters.Length);

			GameObject monster = monsters [monsterType];

			Instantiate (monster, spawnPoint.position, spawnPoint.rotation);

			yield return new WaitForSeconds (2.0f);
		}
	}
}
