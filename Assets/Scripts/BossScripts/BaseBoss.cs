using UnityEngine;
using System.Collections;

public class BaseBoss : MonoBehaviour
{

    protected float left, top, right, bottom; // for easy shit (HA!)

    // Use this for initialization
    void Start()
    {
        left = InterestingGameStuff.left;
        top = InterestingGameStuff.top;
        right = InterestingGameStuff.right;
        bottom = InterestingGameStuff.bottom;

        Initialize(); // per class overridden init
    }

    virtual protected void Initialize()
    {
        // woohoo!
    }

    // Update is called once per frame
    void Update()
    {

    }
}
