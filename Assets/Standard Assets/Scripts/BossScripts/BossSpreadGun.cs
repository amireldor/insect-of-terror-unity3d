using UnityEngine;
using System.Collections;

public class BossSpreadGun : BaseBoss
{
    public float fire_delay  = 0.4f;
    public float rotation_speed = 24.0f;
    public float change_behavior_rate = 2.3f;
    public float enemy_angle = 24.0f;
    
    public GameObject enemy_prefab;
    public float enemy_speed = 2.0f;

    private delegate void StateBehavior();
    private StateBehavior behavior;

    // Use this for initialization
    override protected void Initialize()
    {
        Vector3 new_pos = new Vector3((right) - transform.localScale.x * 0.4f, (top + bottom) / 2, 0);
        transform.position = new_pos;
        behavior = null;
        StartShooting();
    }

    void StartShooting()
    {
        behavior = null;
        InvokeRepeating("Shoot", 0.0f, fire_delay);
        Invoke("StartTurning", change_behavior_rate);
    }

    void StartTurning()
    {
        behavior = TurnStep;
        Invoke("StopTurning", change_behavior_rate);
    }

    void StopTurning()
    {
        behavior -= TurnStep;
        Invoke("StartTurning", change_behavior_rate);
    }

    void Shoot()
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

        // up enemy
        new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        new_enemy.transform.rotation = transform.rotation;
        new_enemy.transform.Translate(translate_vector);
        new_enemy.transform.Rotate(new Vector3(0, 0, 90 - enemy_angle));

        // down enemy
        new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        new_enemy.transform.rotation = transform.rotation;
        new_enemy.transform.Translate(translate_vector);
        new_enemy.transform.Rotate(new Vector3(0, 0, 90 + enemy_angle));
    }

    void TurnStep()
    {
        transform.Rotate(new Vector3(0, 0, -1) * rotation_speed * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        behavior();
    }
}
