using UnityEngine;
using System.Collections;

public class BackbroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int my_level = InterestingGameStuff.level;

        // change background image according to level
        SpriteRenderer bg_renderer = this.GetComponent<SpriteRenderer>();
        Sprite bg_sprite = Resources.Load<Sprite>("backgrounds/level" + my_level);
        bg_renderer.sprite = bg_sprite;

        ScaleToScreenSize();
	}

    // Will scale myself to screen size to cover all viewport
    public void ScaleToScreenSize()
    {
        SpriteRenderer bg_renderer = this.GetComponent<SpriteRenderer>();

        // cover screen
        Vector3 screen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 zeroPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)); // origin of screen in world points
        this.transform.position = zeroPos + new Vector3(0, 0, 10); // move us there
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(screen); // the position of the end of screen in world points
        Vector3 sizePos = screenPos - zeroPos; // the whole size in world points
        Vector3 bounds = bg_renderer.sprite.bounds.max; // sprite size, world points
        Vector3 scaleVec = new Vector3(sizePos.x / bounds.x, sizePos.y / bounds.y, 1); // how much to scale?

        this.transform.localScale = scaleVec;
    }

	// Update is called once per frame
	void Update () {
	}
} 