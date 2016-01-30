using UnityEngine;
using System.Collections;

/**
 * Attach a sprite to something toggle'able.
 * Switches between the original sprite image, and the one set here as alternative
 */
public class ToggledZ : MonoBehaviour {

	private float z_original;
	private bool alt = false;
	
	public float z_alternative;
	public Clickable toggle;
	
	// Use this for initialization
	void Start () {
		this.z_original = this.transform.position.z;
		
		ClickRcvr cr = this.change;
		this.toggle.onClick(cr);
	}
	
	
	void change()
	{
		Vector3 p = this.transform.position;

		Debug.Log ("Toggle Z: " + p.ToString());

		this.alt = !this.alt;
		
		if (this.alt) {
			this.transform.position = new Vector3(p.x, p.y, this.z_alternative);
		} else {
			this.transform.position = new Vector3(p.x, p.y, this.z_original);
		}

		Debug.Log (this.transform.position.ToString());
	}
	
}