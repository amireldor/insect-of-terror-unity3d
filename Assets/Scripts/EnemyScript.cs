using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    private Vector3 rotationVector;
    private float rotation_speed;
    private float left, top, right, bottom;
    private float speed;
    private Sprite final_sprite;
    private bool shitted;
    private System.DateTime shittedTime;
    public float fade_rate = 2.4f;

    // Use this for initialization
    void Start()
    {
        shitted = false;
        StartCoroutine("FadeIn");
    }

    public void randomRotationVector()
    {
        rotationVector = new Vector3(0, 0, (-rotation_speed / 2) + (Random.value * rotation_speed));
        //rotationVector = new Vector3 (0, 0, 50);
    }

    /// <summary>
    /// Changes the look, i.e. change the image that appears on the enemy:
    /// roach, fly, etc...
    /// </summary>
    /// <param name="level">Level.</param>
    public void Initialize(int level, float rotation_speed_in, float speed_in, float left_in, float top_in, float right_in, float bottom_in)
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = InterestingGameStuff.FindEnemySprite(level);
        final_sprite = InterestingGameStuff.FindEnemySprite(level, true); // pooped-upon sprite

        rotation_speed = rotation_speed_in;
        speed = speed_in;
        left = left_in;
        top = top_in;
        right = right_in;
        bottom = bottom_in;
    }

    /// <summary>
    /// Should be called when shit hits the enemy
    /// </summary>
    public void Shitted()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = final_sprite;
        shitted = true;
        shittedTime = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!shitted)
        {
            transform.Rotate(rotationVector * Time.fixedDeltaTime);
            transform.position += transform.up * speed * Time.fixedDeltaTime;

            Vector3 position = transform.position;

            // wrap around world
            if (position.x < left)
            {
                Vector3 newPos = position;
                newPos.x = right;
                StartCoroutine("WrapAroundWorld", newPos);
            }
            if (position.x > right)
            {
                Vector3 newPos = position;
                newPos.x = left;
                StartCoroutine("WrapAroundWorld", newPos);
            }
            if (position.y < top)
            {
                Vector3 newPos = position;
                newPos.y = bottom;
                StartCoroutine("WrapAroundWorld", newPos);
            }
            if (position.y > bottom)
            {
                Vector3 newPos = position;
                newPos.y = top;
                StartCoroutine("WrapAroundWorld", newPos);
            }
            transform.position = position;
        }
    }

    /// <summary>
    /// Will fade out the enemy and fade in the new position given.
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    IEnumerator WrapAroundWorld(Vector3 newPosition)
    {
        float alpha = 1;
        const float alpha_change = 2.4f;
        while (alpha > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            alpha -= alpha_change * Time.deltaTime;
            yield return null;
        }
        this.transform.position = newPosition;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        float alpha = 0;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        while (alpha < 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            alpha += fade_rate * Time.deltaTime;
            yield return null;
        }
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void MowedByLawnMower()
    {
        if (shitted)
        {
            MasterScript master = GameObject.Find("/MasterObject").GetComponent<MasterScript>();
            master.SendMessage("ScoreUp", 20);
            Destroy(this.gameObject);
        }
    }

    public bool IsShitted()
    {
        return shitted;
    }

    public System.DateTime ShittedTime {
        get { return shittedTime; }
    }
}
