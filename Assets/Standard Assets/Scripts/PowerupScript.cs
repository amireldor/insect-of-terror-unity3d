using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour
{
    public float speed = 1.0f;

    private Vector3 target;

    public enum Powerup
    {
        Random,
        Rifle,
        Time,
        Bomb,
    }

    Powerup type;
    Sprite sprite;

    delegate void PowerupAction();
    PowerupAction powerup_action = null;

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
        Initialize();
    }

    public void Initialize(Powerup type_in = Powerup.Random)
    {
        if (type_in == Powerup.Random)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    type = Powerup.Rifle;
                    break;
                case 1:
                    type = Powerup.Time;
                    break;
                case 2:
                    type = Powerup.Bomb;
                    break;
            }
        }
        else
        {
            type = type_in;
        }

        // set powerup specific view + behavior here
        Sprite[]  sprites = Resources.LoadAll<Sprite>("items");
        switch (type)
        {
            case Powerup.Rifle:
                sprite = sprites[1];
                powerup_action = ApplyRifle;
                break;
            case Powerup.Time:
                sprite = sprites[2];
                powerup_action = ApplyTime;
                break;
            case Powerup.Bomb:
                sprite = sprites[3];
                powerup_action = ApplyBomb;
                break;
        }

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = sprite;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
        if ((transform.position - target).sqrMagnitude < Mathf.Pow(0.3f, 2))
        {
            Destroy(this.gameObject);
        }
    }

    public void ApplyPowerup()
    {
        powerup_action();
        Destroy(this.gameObject);
    }

    void ApplyRifle()
    {
        GameObject hero = GameObject.Find("/Hero");
        hero.GetComponent<BossHero>().UpgradeWeapon();
    }

    void ApplyTime()
    {
        GameObject master = GameObject.Find("/MasterBossObject");
        master.GetComponent<MasterBossScript>().BunosTime();
    }

    void ApplyBomb()
    {
        GameObject master = GameObject.Find("/MasterBossObject");
        master.GetComponent<MasterBossScript>().KillEnemies();
    }
}
