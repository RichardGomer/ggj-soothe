using UnityEngine;
using System.Collections;

public class Sink : Clickable {

	private RcvrHandler scrubs;

	// Use this for initialization
	void Start () {
		this.scrubs = new RcvrHandler ();
	}
	
	// Update is called once per frame
	const int UP = 1;
	const int DOWN = 2;

	private int lastScrubY = -1000;
	private int scrubDirection = UP;
	private int scrubCount = 0;

	public new void Update () {

		int currentY = System.Convert.ToInt32 (Input.mousePosition.y);

		if (this.mouseWentDown ()) {
			lastScrubY = currentY;
			Debug.Log ("Begin scrubbing " + this.lastScrubY.ToString ());
		}

		if(this.mouseIsDown())
		{
			Debug.Log ("Scrubbing " + this.lastScrubY.ToString ());

			if(this.scrubDirection == UP && currentY > this.lastScrubY)
			{
				this.scrubCount++;
				this.scrubDirection = DOWN;
				Debug.Log ("Scrub++");
			}
			else if(this.scrubDirection == DOWN && currentY < this.lastScrubY)
			{
				this.scrubCount++;
				this.scrubDirection = UP;
				Debug.Log ("Scrub++");
			}

			if(this.scrubCount > 2)
			{
				this.scrubs.trigger();
				this.scrubCount = 0;
				Debug.Log ("Scrubbed!");
			}
		}
	}

	public void onScrub(ClickRcvr r)
	{
		this.scrubs.add (r);
	}

	public override void unsubscribe(object o)
	{
		base.unsubscribe (o);
		this.scrubs.unsubscribe (o);
	}
}
