using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PooScript : MonoBehaviour
{

    private float final_height; // y in the final vector, used to clamp stuff
    public float height = 1.4f;
    public float speed;
    static LawnMowerScript lawnMowerScript;

    private HashSet<GameObject> collidingGameObjects;

    // Use this for initialization
    void Start()
    {
        final_height = transform.position.y - height;
        transform.Rotate(new Vector3(0, 0, Random.value * 360.0f));
        collidingGameObjects = new HashSet<GameObject>();
        if (!lawnMowerScript) {
            lawnMowerScript = GameObject.Find("/Lawn Mower").GetComponent<LawnMowerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy") {
            // add enemy to list only if it has not been pooped upon
            EnemyScript enemyScript = coll.GetComponent<EnemyScript>();
            if (enemyScript != null) {
                if (!enemyScript.IsShitted()) {
                    collidingGameObjects.Add(coll.gameObject);
                }
            }
        } else {
            collidingGameObjects.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log ("out ID " + coll.gameObject.GetInstanceID());
        collidingGameObjects.Remove(coll.gameObject);
    }

    void FixedUpdate()
    {
        Vector3 new_position = transform.position;
        new_position.y -= speed * Time.fixedDeltaTime;
        if (new_position.y < final_height)
        {
            new_position.y = final_height;

            // poop all enemies
            foreach (GameObject obj in collidingGameObjects)
            {
                if (obj)
                {
                    obj.SendMessage("Shitted");
                }
            }

            Destroy(this.gameObject);
            return;
        }
        transform.position = new_position;
    }
}
