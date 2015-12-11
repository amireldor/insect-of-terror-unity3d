using UnityEngine;
using System.Collections;

public static class InterestingGameStuff
{
    public static int level = 0;
    public static float left = -4, top = -3, right = +4, bottom = +3; // how do I expose this to Unity UI?

    private static Sprite[] enemy_sprites = null;

    public static Sprite FindEnemySprite(int level, bool pooped = false)
    {
        if (enemy_sprites == null)
        {
            enemy_sprites = Resources.LoadAll<Sprite>("enemies");
        }
        Sprite final_sprite;
        level = Mathf.Clamp(level, 0, enemy_sprites.Length - 1);
        if (!pooped)
        {
            final_sprite = enemy_sprites[level];
        }
        else
        {
            final_sprite = enemy_sprites[level + 10]; // pooped-upon sprite
        }

        return final_sprite;
    }
}

public class SharedScript : MonoBehaviour {
/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 */
}
