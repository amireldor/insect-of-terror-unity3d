using UnityEngine;
using System.Collections;

public class BossPoo : MonoBehaviour
{

    public float speed = 0.6f;

    // Use this for initialization
    void Start()
    {

    }
    
    // call after instantiating
    public void Initialize(float poo_speed)
    {
        speed = poo_speed;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(1, 0, 0) * speed;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, movement.normalized, movement.magnitude);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Enemy")
            {
                Destroy(this.gameObject);
            }
        }

        transform.position += movement * Time.fixedDeltaTime;
    }
}
