using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Twirl twirl;

    // Use this for initialization
    void Start()
    {
        twirl = GetComponent<Twirl>();
    }

    public IEnumerator TwirlUp()
    {
        for (float f = 0; f < 180.0f; )
        {
            f += 200.0f * Time.deltaTime;
            twirl.angle = f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
