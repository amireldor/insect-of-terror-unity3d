using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LawnMowerScript : MonoBehaviour
{

    public float speed = 3.0f;
    public float rotationSpeed = 260.0f;
    public int maxPoopedUpon = 8; // how many poops can slow the lawnmower in the same time
    public GameObject disappearPoop; // the poop that is pooped upon the lanwmower

    private Vector3 velocity = Vector3.zero;
    private int poopsUpon = 0;

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
        GameObject[] poopedEnemies = GameObject.FindGameObjectsWithTag("Enemy") as GameObject[];

        // make a list of all pooped enemies in their time
        List<KeyValuePair<System.DateTime, Vector3>> whenAndWhere = new List<KeyValuePair<System.DateTime, Vector3>>();
        foreach (var pooped in poopedEnemies) {
            var script = pooped.GetComponent<EnemyScript>();
            if (script != null && script.IsShitted()) {
                whenAndWhere.Add(new KeyValuePair<System.DateTime, Vector3>(script.ShittedTime, pooped.transform.position));
            }
        }
        // sort by time
        whenAndWhere.Sort( (a, b) => -a.Key.CompareTo(b.Key) );
        
        // start moving
        if (whenAndWhere.Count == 0)
        {
            this.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            float actualSpeed = (1 - (float)poopsUpon / (float)maxPoopedUpon) * speed; // we slow down if pooped upon
            Debug.Log("Actualy speed " + actualSpeed);
            if (actualSpeed < 0) {
                actualSpeed = 0; // just in case
            }
            float turnSpeed = rotationSpeed * Time.fixedDeltaTime;
            Vector3 to = whenAndWhere[whenAndWhere.Count - 1].Value;
            Quaternion newRotation = Quaternion.LookRotation(transform.position - to, Vector3.forward);
            newRotation.x = newRotation.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, turnSpeed);
            transform.Translate(Vector3.up * actualSpeed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
		{
            coll.SendMessage("MowedByLawnMower");
        }
    }

    public void Shitted()
    {
        if (poopsUpon < maxPoopedUpon) {
            poopsUpon++;
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.value * 180.0f);
            GameObject newPoop = Instantiate(disappearPoop, this.transform.position, randomRotation) as GameObject;
            newPoop.transform.parent = this.transform;
        }
    }

    public void DisappearPoopDisappeared() {
        if (poopsUpon > 0) {
            poopsUpon--;
        }
    }
} 