/*
 * BossHero - script for the Hero fighting the boss, that little purple uniformed man of might.
 * 
 */
using UnityEngine;
using System.Collections;

public class BossHero : MonoBehaviour {

    public float get_up_delay = 1.2f;

    private float top, bottom;
    private bool fallen = false;

	// Use this for initialization
	void Start () {
        top = InterestingGameStuff.top;
        bottom = InterestingGameStuff.bottom;
        Sprite my_sprite = GetComponent<SpriteRenderer>().sprite;
        Vector3 new_pos = new Vector3(InterestingGameStuff.left + my_sprite.bounds.size.x / 2.0f, 0, 0);
        transform.position = new_pos;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, new Vector3(1, 1,0));
        Debug.DrawRay(transform.position, Vector3.up * 5);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            FallDown();
        }
    }

    // Goes into 'paralyzed' fallen down state, invokes a delayed 'get up' functions
    void FallDown()
    {
        fallen = true;

        Quaternion new_rot = Quaternion.LookRotation(new Vector3(5, 0, 0), Vector3.forward);
        new_rot.x = new_rot.y = 0.0f;
        transform.rotation = new_rot;

        CancelInvoke("GetUp"); // start fresh!
        Invoke("GetUp", get_up_delay);
    }

    void GetUp()
    {
        fallen = false;
        transform.rotation = Quaternion.identity;
    }
}
