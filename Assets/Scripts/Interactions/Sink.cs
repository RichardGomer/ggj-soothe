using UnityEngine;
using System.Collections;
using System;

public class Sink : Clickable {

	private RcvrHandler scrubs;

	// Use this for initialization
	void Start () {
		this.scrubs = new RcvrHandler ();
	}

	public static explicit operator Sink(GameObject v)
	{
		try
		{
			return v.GetComponent<Sink>();
		}
		catch(NullReferenceException e)
		{
			Debug.LogError(v.name + " is not a Sink!");
			throw e;
		}
	}

	// Update is called once per frame
	const int UP = 1;
	const int DOWN = 2;

	private int lastScrubY;
	private int scrubDirection = UP;
	private int scrubCount = 0;

	public new void Update () {

		int currentY = System.Convert.ToInt32 (Input.mousePosition.y);

		if (this.mouseWentDown ()) {
			this.lastScrubY = currentY;
		}

		if(this.mouseIsDown())
		{
			if(this.scrubDirection == UP && currentY > this.lastScrubY)
			{
				this.scrubCount++;
				this.scrubDirection = DOWN;
			}
			else if(this.scrubDirection == DOWN && currentY < this.lastScrubY)
			{
				this.scrubCount++;
				this.scrubDirection = UP;
			}

			if(this.scrubCount > 2)
			{
				this.scrubs.trigger();
				this.scrubCount = 0;
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
