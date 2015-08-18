using UnityEngine;
using System.Collections;

public class Boss2Script : BaseBoss
{
    public float start_after = 3.0f;

    // Use this for initialization
    override protected void Initialize()
    {
        Vector3 new_pos = new Vector3((left + right) / 2, (top + bottom) / 2, 0);
        transform.position = new_pos;
        StartCoroutine("StupidCoroutine");
    }

    IEnumerator StupidCoroutine()
    {
        yield return new WaitForSeconds(start_after);
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, 360.0f * Time.fixedDeltaTime));
            yield return null;
        }
    }

    void FixedUpdate()
    {
    }
}
