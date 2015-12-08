using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextColorScript : MonoBehaviour {
    Text text;

    public Color target_color;
    public float change_speed = 0.4f;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        target_color = text.color; // initially don't change the color
	}

	// Update is called once per frame
	void Update () {
        Color new_color = new Color();
        new_color = Color.Lerp(text.color, target_color, change_speed * Time.deltaTime);
        text.color = new_color;
	}
}
