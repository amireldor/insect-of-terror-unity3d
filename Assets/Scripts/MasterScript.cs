using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class InterestingGameStuff
{
    public static int level = 0;
}

public class MasterScript : MonoBehaviour
{

    public GameObject enemyPrefab;
    public CameraScript camera_script;

    public float left, top, right, bottom;
    public float enemy_rotation_speed;
    public float enemy_speed;
    public float create_enemy_delay = 0.7f;
    public int max_enemies = 30;

    public Text score_text;
    public Text countdown_text;
    private int score;
    public int score_max = 500;
    public int countdown_start = 30;
    private int countdown;
    private int my_level;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("RotateEnemies", 0, 0.02f);
        InvokeRepeating("CreateRandomEnemy", 0, create_enemy_delay);
        UpdateScoreText();
        countdown = countdown_start;
        UpdateCountdownText();
        InvokeRepeating("CountdownBeat", 1.0f, 1.0f);
        my_level = InterestingGameStuff.level;
        score_max += (score_max / 10) * my_level;
        UpdateScoreText();

        // change background image
        GameObject bg = GameObject.Find("/Background");
        SpriteRenderer bg_renderer = bg.GetComponent<SpriteRenderer>();
        Sprite bg_sprite = Resources.Load<Sprite>("backgrounds/level" + my_level);
        bg_renderer.sprite = bg_sprite;
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

    void CreateRandomEnemy()
    {
        // limit active enemies number
        int counter = 0;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyScript script = obj.GetComponent<EnemyScript>();
            if (!script || script.IsShitted()) continue;
            counter++;
        }
        if (counter >= max_enemies)
        {
            return;
        }

        Vector3 new_pos = Vector3.zero;
        switch (Random.Range(0, 4))
        {
            // position in one of edges of screen
            case 0:
                new_pos.x = left;
                new_pos.y = top + (bottom - top) * Random.value;
                break;
            case 1:
                new_pos.x = right;
                new_pos.y = top + (bottom - top) * Random.value;
                break;
            case 2:
                new_pos.x = left + (right - left) * Random.value;
                new_pos.y = top;
                break;
            case 3:
                new_pos.x = left + (right - left) * Random.value;
                new_pos.y = bottom;
                break;
        }

        GameObject new_enemy = Instantiate(enemyPrefab, new_pos, Quaternion.identity) as GameObject;
        // call Initialize
        new_enemy.GetComponent<EnemyScript>().Initialize(my_level, enemy_rotation_speed, enemy_speed, left, top, right, bottom);
        new_enemy.transform.Rotate(new Vector3(0, 0, Random.value * 360));
    }

    void ScoreUp(int howmuch = 1)
    {
        score += howmuch;
        UpdateScoreText();

        // end level?
        if (score >= score_max)
        {
            InterestingGameStuff.level++;
            Application.LoadLevel("level");
        }
    }

    void UpdateScoreText()
    {
        score_text.text = "Score: " + score + "/" + score_max;
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

    // Update is called once per frame
    void Update()
    {

    }
}
