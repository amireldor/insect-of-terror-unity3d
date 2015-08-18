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

    // Use this for initialization
    void Start()
    {
        shitted = false;
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
                position.x = right;
            }
            if (position.x > right)
            {
                position.x = left;
            }
            if (position.y < top)
            {
                position.y = bottom;
            }
            if (position.y > bottom)
            {
                position.y = top;
            }
            transform.position = position;
        }
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
}
