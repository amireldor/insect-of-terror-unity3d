using UnityEngine;
using System.Collections;

public class LawnMowerScript : MonoBehaviour
{

    public MasterScript master;
    public float speed = 3.0f;
    public float y_movement_factor = 0.4f;
    private string direction;
    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        Vector3 new_pos = new Vector3(master.left, 0, 0);
        transform.position = new_pos;
        direction = "right";
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && velocity == Vector3.zero)
        {
            if (direction == "right")
            {
                velocity.x = speed;
            }
            else
            {
                velocity.x = -speed;
            }
        }
    }

    void SwitchDirection()
    {
        if (direction == "right")
        {
            direction = "left";
        }
        else
        {
            direction = "right";
        }
        transform.Rotate(new Vector3(0, 0, 180));
    }

    void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            transform.position += velocity * Time.fixedDeltaTime;

            if (transform.position.x > master.right)
            {
                Vector3 new_pos = transform.position;
                new_pos.x = master.right;
                transform.position = new_pos;
                SwitchDirection();
                velocity = Vector3.zero;
            }

            if (transform.position.x < master.left)
            {
                Vector3 new_pos = transform.position;
                new_pos.x = master.left;
                transform.position = new_pos;
                SwitchDirection();
                velocity = Vector3.zero;
            }
        }
        else
        {
            // velocity == Vector3.zero
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dist;
            dist = mouse_pos.y - transform.position.y;
            dist /= y_movement_factor;
            transform.position += new Vector3(0, dist * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.SendMessage("MowedByLawnMower");
    }
}
