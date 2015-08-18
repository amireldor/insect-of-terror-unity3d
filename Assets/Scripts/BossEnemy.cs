/*
 * An enemy with the behavior needed in the boss stage
 */

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    private int level;
    private float speed;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(int level_in, float speed_in)
    {
        level = level_in;
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = InterestingGameStuff.FindEnemySprite(level);

        speed = speed_in;
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
	}
}
