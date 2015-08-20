using UnityEngine;
using System.Collections;

public class BossPoo : MonoBehaviour
{

    public float speed = 0.6f;
    public float rotation_speed = 195.0f;

    public GameObject explosion_prefab;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.value * 360.0f));
    }
    
    // call after instantiating
    public void Initialize(float poo_speed)
    {
        speed = poo_speed;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(1, 0, 0) * speed;
        transform.Rotate(new Vector3(0, 0, 1) * rotation_speed * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized, movement.magnitude);
        if (hit.collider.tag == "Enemy")
        {
            hit.collider.gameObject.SendMessage("Pooed");
            Destroy(this.gameObject);
        }
        else if (hit.collider.tag == "Powerup")
        {
            hit.collider.gameObject.GetComponent<PowerupScript>().ApplyPowerup();
        }
        else if (hit.collider.tag == "Boss")
        {
            Instantiate(explosion_prefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        transform.position += movement * Time.fixedDeltaTime;

        if (transform.position.x > InterestingGameStuff.right)
        {
            Destroy(this.gameObject);
        }
    }
}
