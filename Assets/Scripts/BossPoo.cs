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
        transform.position += movement * Time.fixedDeltaTime;

        if (transform.position.x > InterestingGameStuff.right)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.SendMessage("Pooed");
            Destroy(this.gameObject);
        }
        else if (collider.tag == "Powerup")
        {
           collider.gameObject.GetComponent<PowerupScript>().ApplyPowerup();
        }
        else if (collider.tag == "Boss")
        {
            collider.GetComponent<BaseBoss>().LowerHealth();
            Instantiate(explosion_prefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
