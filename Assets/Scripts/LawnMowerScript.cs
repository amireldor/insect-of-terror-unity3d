using UnityEngine;
using System.Collections;

public class LawnMowerScript : MonoBehaviour
{

    public MasterScript master;
    public float speed = 3.0f;
    public  float rotationSpeed = 260.0f;
    private Vector3 velocity;

    private System.Collections.Generic.List<Vector3> waypoints; // where to go? essentially places of pooped-upon enemies

    LawnMowerScript()
    {
        waypoints = new System.Collections.Generic.List<Vector3>();
    }

    public void AddWaypoint(Vector3 point) {
        waypoints.Add(point);
    }

    // Use this for initialization
    void Start()
    {
        transform.position = Vector3.zero;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SwitchDirection()
    {
    }

    void FixedUpdate()
    {
        if (waypoints.Count == 0)
        {
            this.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            float turnSpeed = 240 * Time.fixedDeltaTime;
            Vector3 to = waypoints[waypoints.Count - 1];
            Quaternion newRotation = Quaternion.LookRotation(transform.position - to, Vector3.forward);
            newRotation.x = newRotation.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, turnSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.SendMessage("MowedByLawnMower");
    }
} 