using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PooScript : MonoBehaviour {

	private float final_height; // y in the final vector, used to clamp stuff
	public float height = 1.4f;
	public float speed;

	private HashSet<GameObject> colliding_enemies;

	// Use this for initialization
	void Start () {
		final_height = transform.position.y - height;
		transform.Rotate (new Vector3(0, 0, Random.value * 360.0f));
		colliding_enemies = new HashSet<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		EnemyScript script = coll.GetComponent<EnemyScript> ();
		if (script != null) {
			if (!script.IsShitted()) {
				colliding_enemies.Add (coll.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		//Debug.Log ("out ID " + coll.gameObject.GetInstanceID());
		colliding_enemies.Remove (coll.gameObject);
	}

	void FixedUpdate() {
		Vector3 new_position = transform.position;
		new_position.y -= speed * Time.fixedDeltaTime;
		if (new_position.y < final_height) {
			new_position.y = final_height;

			// poop all enemies
			foreach (GameObject obj in colliding_enemies) {
                if (obj) obj.SendMessage ("Shitted");
			}

			Destroy(this.gameObject);
			return;
		}
		transform.position = new_position;
	}
}
