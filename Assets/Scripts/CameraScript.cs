using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public float max_twirl = 180.0f;
    public float twirl_speed = 200.0f;

    // Use this for initialization
    void Start()
    {
    }

    public IEnumerator TwirlUp()
    {
        Twirl twirl = GetComponent <Twirl>();
        for (float f = 0; f < max_twirl; )
        {
            f += twirl_speed * Time.deltaTime;
            twirl.angle = f;
            yield return null;
        }
    }

    public IEnumerator TwirlDown()
    {
        Twirl twirl = GetComponent <Twirl>();
        for (float f = max_twirl; f > 0.0f; )
        {
            f -= twirl_speed * Time.deltaTime;
            twirl.angle = f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
