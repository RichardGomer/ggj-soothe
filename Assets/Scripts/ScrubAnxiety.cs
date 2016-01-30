using UnityEngine;
using System.Collections;

public class ScrubAnxiety : Anxiety, Thought {

	private int count = 0;
	private int required = 0;
	private Sink target;
	
	public ScrubAnxiety(Sink target, int required)
	{
		this.required = required;
		this.target = target;
		
		// Register for clicks
		ClickRcvr rcv = this.incrementCount;
		this.target.onScrub(rcv);
	}
	
	public string getDescription()
	{
		return "Are my hands contaminated?..";
	}
	
	public void incrementCount()
	{
		Debug.Log ("Scrubbed!");
		this.count++;
	}
	
	public  bool isComplete()
	{
		return this.count > 0 && (this.count % this.required == 0);
	}
	
	public void forget()
	{
		this.target.unsubscribe (this);
	}
}
