using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseBoss : MonoBehaviour
{

    public Text health_text;

    protected float left, top, right, bottom; // for easy shit (HA!)
    protected float health = 100.0f;

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

    public void LowerHealth(float howmuch = 3.0f)
    {
        health -= 3.0f;
        if (health <= 0.0f)
        {
            health = 0.0f;
            // boss is dead
            // TODO: initiate death sequence
        }
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        int i_health = (int) Mathf.Ceil(health);
        health_text.text = "BOSS HEALTH: " + i_health;
    }

}
