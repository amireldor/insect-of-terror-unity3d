/*
 * An enemy with the behavior needed in the boss stage
 */

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    private int level;
    private float speed;

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
        transform.position += transform.up * speed * Time.fixedDeltaTime;

        if (transform.position.x < left ||
            transform.position.y < top ||
            transform.position.x > right ||
            transform.position.y > bottom)
        {
            Destroy(this.gameObject);
        }
	}
}
