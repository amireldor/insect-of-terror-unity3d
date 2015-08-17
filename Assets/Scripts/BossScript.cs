using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    // not sure if this is the way to go, because there are animator state machines and stuff...
    // but they don't seem to fit
    private delegate void StateBehavior();

    private StateBehavior behavior;

    public float rotation_speed_normal = 30.0f;
    public float rotation_speed_fire =360.0f;
    public Vector3 sway_vector = new Vector3(-0.3f, -4, 0); // boss sways stupidly when idle

    private Vector3 target_vector; // go to this rotation at rotation speed
    private float rotation_speed;

    // Use this for initialization
    void Start()
    {
        Vector3 new_pos = new Vector3();
        new_pos.x = InterestingGameStuff.right - 1.0f;
        new_pos.y = InterestingGameStuff.top + (InterestingGameStuff.bottom - InterestingGameStuff.top) / 2.0f;
        new_pos.z = 0;
        transform.position = new_pos;

        rotation_speed = rotation_speed_normal;
        target_vector = sway_vector;
        InvokeRepeating("SwitchHappyToRotation", 0.6f, 0.6f);
        behavior = RotateHappily;
    }

    void BasicBehavior()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.fixedDeltaTime));
        if (transform.rotation.eulerAngles.z > 120.0f)
        {
            behavior -= BasicBehavior;
            behavior += TestBehavior;
        }
    }

    void DoRotation()
    {
        Vector3 target = target_vector + transform.position;
        Quaternion new_rotation = Quaternion.LookRotation(target - transform.position, Vector3.forward);
        new_rotation.x = new_rotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, new_rotation, rotation_speed * Time.fixedDeltaTime);
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
