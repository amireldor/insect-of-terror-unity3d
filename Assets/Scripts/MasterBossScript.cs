using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MasterBossScript : MonoBehaviour
{

//    public GameObject enemyPrefab;
    public CameraScript camera_script;

    public float left, top, right, bottom;
    public float enemy_rotation_speed;
    public float enemy_speed;
    public float create_enemy_delay = 0.7f;
    public int max_enemies = 30;

  /*  public Text score_text;
    private int score;
    public int score_max = 500;
    */
    public Text countdown_text;
    public int countdown_start = 30;
    private int countdown;
    private int my_level;

    private bool finish_started = false;

    // Use this for initialization
    void Start()
    {
        left = InterestingGameStuff.left;
        top = InterestingGameStuff.top;
        right = InterestingGameStuff.right;
        bottom = InterestingGameStuff.bottom;

/*        InvokeRepeating("RotateEnemies", 0, 0.02f);
        InvokeRepeating("CreateRandomEnemy", 0, create_enemy_delay);
        UpdateScoreText();
        countdown = countdown_start;
        UpdateCountdownText();
        InvokeRepeating("CountdownBeat", 1.0f, 1.0f);
        score_max += (score_max / 10) * my_level;
        UpdateScoreText();*/
        my_level = InterestingGameStuff.level;

        // change background image
        GameObject bg = GameObject.Find("/Background");
        SpriteRenderer bg_renderer = bg.GetComponent<SpriteRenderer>();
        Sprite bg_sprite = Resources.Load<Sprite>("backgrounds/level" + my_level);
        bg_renderer.sprite = bg_sprite;

        /*
        // Example code: start differnet boss script:
        GameObject boss = GameObject.Find("/Boss");
        Destroy(boss.GetComponent<BossScript>());
        var new_script = boss.AddComponent<Boss2Script>();
        new_script.start_after = 0.4f;
        */
    }

    /// <summary>
    /// Iterates through enemies and changes their rotationVector,
    /// for an awesome wiggly moving effect.
    /// </summary>
    void RotateEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyScript script = enemy.GetComponent<EnemyScript>();
            if (script)
            { // for some reason it was null on game very start, so ignore if null
                script.randomRotationVector();
            }
        }
    }

    void UpdateCountdownText()
    {
        countdown_text.text = "Time: " + countdown;
    }

    void CountdownBeat()
    {
        countdown--;
        if (countdown <= 0)
        {
            countdown = 0;
            // level over
        }
        UpdateCountdownText();
    }

    // note! I had a bug when this was in `Start` (where it shouldn't be). Probably `twirl` on camera_script was not yet set up.
    public IEnumerator FinishLevel()
    {
        // collect 'bunos' time
        // show cool stuff
        // only do this once
        if (!finish_started)
        {
            finish_started = true;
            yield return StartCoroutine(camera_script.TwirlUp());
            yield return StartCoroutine("_FinalAction");
        }
    }

    // Final commands to invoke when finishing level, called in `FinishLevel`
    IEnumerator _FinalAction()
    {
        Application.LoadLevel("level");
        InterestingGameStuff.level++;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
