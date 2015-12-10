using UnityEngine;
using System.Collections;

public class LawnMowerScript : MonoBehaviour
{

    public MasterScript master;
    public float speed = 3.0f;
    public float rotationSpeed = 260.0f;
    private Vector3 velocity = Vector3.zero;

    private System.Collections.Generic.List<Vector3> waypoints = new System.Collections.Generic.List<Vector3>(); // where to go? essentially places of pooped-upon enemies

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
            float turnSpeed = rotationSpeed * Time.fixedDeltaTime;
            Vector3 to = waypoints[waypoints.Count - 1];
            Quaternion newRotation = Quaternion.LookRotation(transform.position - to, Vector3.forward);
            newRotation.x = newRotation.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, turnSpeed);
            Debug.Log(transform.up);
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);

            if (Vector3.SqrMagnitude(transform.position - to) < Mathf.Pow(0.01f, 2))
            {
                // arrived at waypoint
                waypoints.Remove(waypoints[waypoints.Count - 1]);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.SendMessage("MowedByLawnMower");
    }
} 