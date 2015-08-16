using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {

	public GameObject enemyPrefab;

	public float left, top, right, bottom;
	public float enemy_rotation_speed;
	public float enemy_speed;

	// Use this for initialization
	void Start () {
		for (int i=0; i<10;i++) {
			GameObject obj = Instantiate(enemyPrefab, new Vector3(-2 + Random.value* 4, -2 + Random.value * 4, 0), Quaternion.identity) as GameObject;

			// call Initialize
			obj.GetComponent<EnemyScript>().Initialize(0, enemy_rotation_speed, enemy_speed, left, top, right, bottom);
			obj.transform.Rotate(new Vector3(0, 0, Random.value * 360));
		}

		InvokeRepeating ("RotateEnemies", 0, 0.02f);
	}

	/// <summary>
	/// Iterates through enemies and changes their rotationVector,
	/// for an awesome wiggly moving effect.
	/// </summary>
	void RotateEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			EnemyScript script = enemy.GetComponent<EnemyScript>();
			if (script) { // for some reason it was null on game very start, so ignore if null
				script.randomRotationVector();
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
