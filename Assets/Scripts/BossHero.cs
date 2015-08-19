/*
 * BossHero - script for the Hero fighting the boss, that little purple uniformed man of might.
 * 
 */
using UnityEngine;
using System.Collections;

public class BossHero : MonoBehaviour {

    float top, bottom;

	// Use this for initialization
	void Start () {
        top = InterestingGameStuff.top;
        bottom = InterestingGameStuff.bottom;
        Sprite my_sprite = GetComponent<SpriteRenderer>().sprite;
        Vector3 new_pos = new Vector3(InterestingGameStuff.left + my_sprite.bounds.size.x / 2.0f, 0, 0);
        transform.position = new_pos;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 20.0f * Time.deltaTime));
	}
}
