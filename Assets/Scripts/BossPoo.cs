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

        transform.position += movement * Time.fixedDeltaTime;

        if (transform.position.x > InterestingGameStuff.right)
        {
            Destroy(this.gameObject);
        }
    }
}
