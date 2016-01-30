using UnityEngine;
using System.Collections;

/**
 * Attach a sprite to something toggle'able.
 * Switches between the original sprite image, and the one set here as alternative
 */
public class ToggledSprite : MonoBehaviour {

	private SpriteRenderer srend;
	private Sprite sprite_original;
	private bool alt = false;

	public Sprite sprite_alternative;
	public Clickable toggle;

	// Use this for initialization
	void Start () {
		this.srend = this.gameObject.GetComponent<SpriteRenderer> ();
		this.sprite_original = srend.sprite;

		ClickRcvr cr = this.change;
		this.toggle.onClick(cr);
	}


	void change()
	{
		this.alt = !this.alt;

		if (this.alt) {
			this.srend.sprite = this.sprite_alternative;
		} else {
			this.srend.sprite = this.sprite_original;
		}
	}

}
