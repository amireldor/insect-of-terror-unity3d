using UnityEngine;
using System.Collections;

public class AssScript : MonoBehaviour {

	public GameObject pooObject;
	public float poop_delay;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreatePoop", 0, poop_delay);
	}

	void CreatePoop() {
		GameObject poo = Instantiate (pooObject);
		Vector3 new_pos = transform.position;
		poo.transform.position = new_pos;
	}

	// Update is called once per frame
	void Update () {
		Vector3 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		position.z = 0;
		transform.position = position;
	}
}
