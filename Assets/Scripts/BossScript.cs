using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    // not sure if this is the way to go, because there are animator state machines and stuff...
    // but they don't seem to fit
    private delegate void StateBehavior();

    private StateBehavior behavior;

    public float rotation_speed_normal = 30.0f;
    public float rotation_speed_fire = 360.0f;
    public float goto_speed = 0.8f;
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

        rotation_speed = rotation_speed_normal;
        target_vector = sway_vector;
        // probably use time values in following Invoking in some setting or public variable
        InvokeRepeating("SwitchHappyToRotation", 0.6f, 0.6f);
        InvokeRepeating("RandomizeGoTo", 1.4f, 1.4f);
        behavior += RotateHappily;
        behavior += JumpToGoTo;
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

        goto_vector = new Vector3(right - ((right - left) * 0.2f), top + Random.value * (bottom - top), 0);
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

    void TestBehavior()
    {
        transform.localScale += new Vector3(0.1f * Time.fixedDeltaTime, 0, 0);
    }

    void RandomJump()
    {
    }

    void FixedUpdate()
    {
        behavior();
    }
}
