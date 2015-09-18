using UnityEngine;
using System.Collections;

public class BossTripleShot : BaseBoss {

    public float fire_delay  = 0.78f;
    public int shots_per_shooting = 4; // how many shots are fired each time the boss shoots
    public float change_behavior_rate = 2.3f;
    public float movement_speed = 0.12f;
    
    public GameObject enemy_prefab;
    public float enemy_speed = 1.7f;

    private delegate void StateBehavior();
    private StateBehavior behavior;

    private Vector3 target_position;

	// Use this for initialization
	void Start () {
        Vector3 new_pos = new Vector3((InterestingGameStuff.right) - transform.localScale.x * 0.62f, 0, 0);
        transform.position = new_pos;
        Fire();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Start firing sequence
    void Fire()
    {
        int shots = shots_per_shooting;
        while (shots-- > 0)
        {
            Invoke("SingleShot", shots * fire_delay); // each shot has increasing delay
        }
    }

    void RandomMove()
    {
        target_position = new Vector3(0, (InterestingGameStuff.top + Random.value * (InterestingGameStuff.down - InterestingGameStuff.top), 0);
        behavior = MoveTowardsTarget;
    }

    void MoveTowardsTarget()
    {
        Vector3 movement_vector = target_position - transform.position;
        // limit movement magnitude to `movement_speed
        if (movement_vector.sqrMagnitude >= movement_speed)
        {
        }
    }

    void SingleShot()
    {
        Vector3 new_pos = transform.position;
        //Vector3 translate_vector = new Vector3(-transform.localScale.x / 2, 0, 0);
        Vector3 translate_vector = new Vector3(-1, 0, 0);
        GameObject new_enemy;
        // fire 3 enemies in 3 angles
        // straight enemy
        new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        new_enemy.transform.rotation = transform.rotation;
        new_enemy.transform.Translate(translate_vector);
        new_enemy.transform.Rotate(new Vector3(0, 0, 90));

        float vertical_translation = transform.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2.0f;

        // up enemy
        new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        new_enemy.transform.rotation = transform.rotation;
        new_enemy.transform.Translate(translate_vector + new Vector3(0, -vertical_translation));
        new_enemy.transform.Rotate(new Vector3(0, 0, 90));

        // down enemy
        new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        new_enemy.transform.rotation = transform.rotation;
        new_enemy.transform.Translate(translate_vector + new Vector3(0, +vertical_translation));
        new_enemy.transform.Rotate(new Vector3(0, 0, 90));
    }
}
