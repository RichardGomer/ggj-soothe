using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	private TextMesh text;

	// Use this for initialization
	void Start () {
		this.text = gameObject.GetComponent<TextMesh> ();
		this.reset ();
	}

	private long now; // Time since start of game in ms
	void Update () {
		this.now = System.Convert.ToInt64( Time.time * 1000 );
		this.draw ();
	}

	private int secondsPerHour = 60; // 60 real seconds per game hour
	private long begin = 0; // Our "Midnight" in game time

	public void reset()
	{
		this.begin = this.now;
		this.draw ();
	}

	private float hourOffset = 6.5f; // Start at 06:30
	public void draw()
	{
		long deltasecs = (now - this.begin) / 1000;

		float hourdelta = ((float) deltasecs / (float) this.secondsPerHour) + this.hourOffset;

		Debug.Log ("Time: " + deltasecs + " / Hours: " + hourdelta.ToString());

		int hours = System.Convert.ToInt16(Mathf.Floor(hourdelta));
		int mins = System.Convert.ToInt16((hourdelta - hours) * 60);

		this.setDisplay (stringify(hours % 24) + ":" + stringify(mins));
	}

	protected string stringify(int num)
	{
		string str = num.ToString ();

		if(num < 10)
		{
			str = "0" + str;
		}

		return str;
	}

	protected void setDisplay(string time)
	{
		this.text.text = time;
	}


}
