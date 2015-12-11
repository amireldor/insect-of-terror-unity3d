using UnityEngine;
using System.Collections;

public class DisappearPoopScript : MonoBehaviour {

    public float disappearAfter = 2.0f;

	// Use this for initialization
	void Start () {
        Invoke("Disappear", disappearAfter);
	}

    void Disappear()
    {
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
