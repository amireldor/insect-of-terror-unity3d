using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    // not sure if this is the way to go, because there are animator state machines and stuff...
    // but they don't seem to fit
    private delegate void StateBehavior();

    private StateBehavior behavior;

    public float rotation_speed_normal = 30.0f;
    public float rotation_speed_fire = 180.0f;
    public float goto_speed = 0.8f;
    public float random_behavior_max_time = 4.0f;
    public Vector3 sway_vector = new Vector3(-0.3f, -4, 0); // boss sways stupidly when idle

    private Vector3 target_vector; // rotate to face this
    private Vector3 goto_vector; // go to this position
    private float rotation_speed;

    // Use this for initialization
    void Start()
    {
        Vector3 new_pos = new Vector3();
        new_pos.x = InterestingGameStuff.right - 1.0f;
        new_pos.y = InterestingGameStuff.top + (InterestingGameStuff.bottom - InterestingGameStuff.top) / 2.0f;
        new_pos.z = 0;
        transform.position = new_pos;
        goto_vector = new_pos;

        StartRegularBehavior();
    }

    void JumpToGoTo()
    {
        transform.position = Vector3.Slerp(transform.position, goto_vector, goto_speed * Time.fixedDeltaTime);
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
        float left, top, right, bottom; // for easy shit (HA!)
        left = InterestingGameStuff.left;
        top = InterestingGameStuff.top;
        right = InterestingGameStuff.right;
        bottom = InterestingGameStuff.bottom;

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
        InvokeRepeating("RandomizeGoTo", 1.4f, 1.4f);
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
    }

    void StopShooting()
    {
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
