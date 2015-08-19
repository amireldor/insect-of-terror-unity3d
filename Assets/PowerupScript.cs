using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour
{
    public float speed = 1.0f;

    private Vector3 target;

    // Use this for initialization
    void Start()
    {
        float left, top, right, bottom;
        left = InterestingGameStuff.left;
        top = InterestingGameStuff.top;
        right = InterestingGameStuff.right;
        bottom = InterestingGameStuff.bottom;

        float horizontal = left + Random.value * (right - left);
        float vertical = top + Random.value * (bottom - top);

        target = new Vector3(horizontal, vertical);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
        if ((transform.position - target).sqrMagnitude < Mathf.Pow(0.3f, 2))
        {
            Destroy(this.gameObject);
        }
    }
}
