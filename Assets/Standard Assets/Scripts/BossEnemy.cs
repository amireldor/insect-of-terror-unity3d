/*
 * An enemy with the behavior needed in the boss stage
 */

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    public float gravity_force = 1.0f;

    private int level;
    private float speed;

    private bool pooed = false;
    protected float left, top, right, bottom; // for easy shit (HA!)

	// Use this for initialization
	void Start () {
        left = InterestingGameStuff.left;
        top = InterestingGameStuff.top;
        right = InterestingGameStuff.right;
        bottom = InterestingGameStuff.bottom;
	}

    public void Initialize(int level_in, float speed_in)
    {
        level = level_in;
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = InterestingGameStuff.FindEnemySprite(level);

        speed = speed_in;
    }

	void FixedUpdate () {
        if (!pooed)
        {
            transform.position += transform.up * speed * Time.fixedDeltaTime;
        }
        else
        {
            transform.position += new Vector3(0, gravity_force, 0) * Time.fixedDeltaTime;
            transform.Rotate(new Vector3(0, 0, 360.0f * 3 * Time.fixedDeltaTime));
        }

        if (transform.position.x < left ||
            transform.position.y < top ||
            transform.position.x > right ||
            transform.position.y > bottom)
        {
            Destroy(this.gameObject);
        }
	}

    void Pooed()
    {
        if (pooed) return;
        pooed = true;
        // change to pooped-upon sprite
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = InterestingGameStuff.FindEnemySprite(level, true);
    }
}
