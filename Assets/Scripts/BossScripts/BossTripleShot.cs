using UnityEngine;
using System.Collections;

public class BossTripleShot : BaseBoss {

    public float fire_delay  = 0.78f;
    public int shots_per_shooting = 4; // how many shots are fired each time the boss shoots
    public float change_behavior_time = 2.3f;
    public float movement_speed = 0.12f;
    public float shiver_value = 0.02f;

    public GameObject enemy_prefab;
    public float enemy_speed = 1.7f;

    private delegate void StateBehavior();
    private StateBehavior behavior;

    private Vector3 target_position;

	// Use this for initialization
	void Start () {
        Vector3 new_pos = new Vector3((InterestingGameStuff.right) - transform.localScale.x * 0.62f, 0, 0);
        transform.position = new_pos;
        behavior = null;
        RandomMove();
	}
	
	void FixedUpdate () {
        if (behavior != null) behavior();
	}

    // Start firing sequence
    void Fire()
    {
        behavior = null;
        Shiver();
        
        int shots = shots_per_shooting;
        while (shots-- > 0)
        {
            Invoke("SingleShot", shots * fire_delay); // each shot has increasing delay
        }

        Invoke("StopShiver", fire_delay * (shots_per_shooting - 1));
        Invoke("RandomMove", fire_delay * (shots_per_shooting - 1) + change_behavior_time);
    }

    void RandomMove()
    {
        target_position = new Vector3(transform.position.x, InterestingGameStuff.top + Random.value * (InterestingGameStuff.bottom - InterestingGameStuff.top), 0);
        behavior = MoveTowardsTarget;
        Invoke("Fire", change_behavior_time);
    }

    void MoveTowardsTarget()
    {
        Vector3 new_pos = Vector3.MoveTowards(transform.position, target_position, movement_speed * Time.fixedDeltaTime);
        transform.position = new_pos;
    }

    void Shiver()
    {
        InvokeRepeating("_ShiverStep", 0, 0.08f);
    }

    void StopShiver()
    {
        CancelInvoke("_ShiverStep");
    }

    void _ShiverStep()
    {
        Vector3 shiver_vec = new Vector3(-0.5f + Random.value, -0.5f + Random.value, 0);
        shiver_vec *= shiver_value * Time.fixedDeltaTime;
        transform.Translate(shiver_vec);
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

        float vertical_translation = transform.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.4f;

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
