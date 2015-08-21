using UnityEngine;
using System.Collections;

public class BossScript : BaseBoss
{
    // not sure if this is the way to go, because there are animator state machines and stuff...
    // but they don't seem to fit
    private delegate void StateBehavior();

    private StateBehavior behavior;

    public GameObject enemy_prefab;


    public float rotation_speed_normal = 30.0f;
    public float rotation_speed_fire = 180.0f;
    public float goto_speed = 0.8f;
    public float random_behavior_max_time = 4.0f;
    public float goto_change_time = 1.0f;
    public float fire_delay = 0.24f;
    public float enemy_speed = 5.0f;
    public Vector3 sway_vector = new Vector3(-0.3f, -4, 0); // boss sways stupidly when idle
    public float enemy_offset = 0.6f;

    private Vector3 target_vector; // rotate to face this
    private Vector3 goto_vector; // go to this position
    private float rotation_speed;

    // called automatically on `Start`
    override protected void Initialize()
    {
        Vector3 new_pos = new Vector3();
        new_pos.x = InterestingGameStuff.right - 1.0f;
        new_pos.y = InterestingGameStuff.top + (InterestingGameStuff.bottom - InterestingGameStuff.top) / 2.0f;
        new_pos.z = 0;
        transform.position = new_pos;
        goto_vector = new_pos;
        health = 100.0f + 20.0f * InterestingGameStuff.level;

        StartRegularBehavior();
        UpdateHealthText();
    }

    void JumpToGoTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, goto_vector, goto_speed * Time.fixedDeltaTime);
    }

    void DoRotation()
    {
        Vector3 target = target_vector + transform.position;
        Quaternion new_rotation = Quaternion.LookRotation(target - transform.position, Vector3.forward);
        new_rotation.x = new_rotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, new_rotation, rotation_speed * Time.fixedDeltaTime);
    }

    void RandomizeGoTo()
    {
        goto_vector = new Vector3(right - Random.value * ((right - left) * 0.4f), top + Random.value * (bottom - top), 0);
    }

    // switch the to_rotation, not the happiness into a rotation
    void SwitchHappyToRotation()
    {
        target_vector.x *= -1;
    }

    // the swaying motion
    void RotateHappily()
    {
        DoRotation();
    }

    // boss starts dancing and will sometime shoot
    void StartRegularBehavior()
    {
        rotation_speed = rotation_speed_normal;
        target_vector = sway_vector;
        // probably use time values in following Invoking in some setting or public variable
        InvokeRepeating("SwitchHappyToRotation", 0.6f, 0.6f);
        InvokeRepeating("RandomizeGoTo", goto_change_time, goto_change_time);
        Invoke("StartShooting", Random.value * random_behavior_max_time);
        behavior = null;
        behavior += RotateHappily;
        behavior += JumpToGoTo;
    }

    // Change the jumping behavior or whatever to SHOOTING tatatatata!
    void StartShooting()
    {
        CancelInvoke("SwitchHappyToRotation");
        behavior = ShootingBehavior;
        rotation_speed = rotation_speed_fire;
        Vector3 new_target = transform.position;
        new_target.x -= 5;
        target_vector = new Vector3(-5, 0 , 0);
        Invoke("StopShooting", Random.value * random_behavior_max_time);
        InvokeRepeating("ShootEnemy", 0.42f, fire_delay);
    }

    void ShootEnemy()
    {
        Vector3 new_pos = transform.position;
        new_pos.x -= transform.localScale.x / 2.0f;
        GameObject new_enemy = Instantiate(enemy_prefab, new_pos, Quaternion.identity) as GameObject;
        new_enemy.GetComponent<BossEnemy>().Initialize(InterestingGameStuff.level, enemy_speed);
        Quaternion new_rotation = Quaternion.LookRotation(new Vector3(5, -enemy_offset + Random.value * enemy_offset * 2, 0), Vector3.forward);
        new_rotation.x = new_rotation.y = 0;
        new_enemy.transform.rotation = new_rotation;
        //new_enemy.transform.Rotate(new Vector3(0, 0, 90.0f));
    }

    void StopShooting()
    {
        CancelInvoke("ShootEnemy");
        StartRegularBehavior();
    }

    void ShootingBehavior()
    {
        DoRotation();
    }

    void FixedUpdate()
    {
        behavior();
    }

}
