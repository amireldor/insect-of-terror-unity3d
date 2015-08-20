using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

    public float time = 0.8f;
    public float rotation_speed = -294.0f;

    // Use this for initialization
    void Start()
    {
        Invoke("EndLife", time);
    }

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, 1) * rotation_speed * Time.fixedDeltaTime);
	}

    void EndLife()
    {
        Destroy(this.gameObject);
    }
}
