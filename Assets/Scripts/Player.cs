using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

    protected ArrayList thoughts;
	public UIFacade UIF;
	public Clock clock;

	// Use this for initialization
	void Start () {

        this.thoughts = new ArrayList();

		this.reset (0);

	}
	
	// Update is called once per frame
	void Update () {
        this.purgeThoughts();

		// Level completes
		if (this.thoughts.Count < 1) {
			this.reset (this.level+1);
		}
    }

	private int level;
	void reset(int level){

		this.level = level;

		// Reset the clock
		// TODO: This currently goes to midnight...
		this.clock.reset ();

		Debug.Log ("Setup level " + level);

		switch (level) {
			case 4:
				this.say ("DONE!", 5000);
			return;

			case 3:
				SecurityAnxiety s1, s2;
				this.addThought(s1 = new SecurityAnxiety((Clickable) GameObject.Find("FrontDoor"), 1));
				s1.chainThought(s2 = new SecurityAnxiety((Clickable) GameObject.Find("Window"), 1));
			return;


			case 2:
				this.addThought(new LightAnxiety((Clickable) GameObject.Find("Switch"), 4));
				this.addThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));
			break;


			case 1:
				this.addThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));	
			break;


			case 0:
				this.addThought(new LightAnxiety((Clickable) GameObject.Find("Switch"), 4));
			break;
		}

	}

    // Convenience method that delegates to the speech system
    public void say(string s, int delay)
    {
        this.GetComponent<Speech>().queueText(s, delay);
    }

    /**
     * Thought handling
     */
    public ArrayList getThoughts()
    {
        return this.thoughts;
    }
    
    public void addThought(Thought t)
    {
        this.thoughts.Add(t);

		this.say (t.getDescription(), 2500);
    }

    public void removeThought(Thought t)
    {
        this.thoughts.Remove(t);
    }

    // Clean up complete thoughts
    protected void purgeThoughts()
    {
		try
		{
		    foreach(Thought tt in this.thoughts)
	        {
				if (tt.isComplete())
	            {
	                this.thoughts.Remove(tt);
	                this.say(tt.getCompletionSpeech(), 1000);

					// See if there are chained thoughts...
					if(tt.hasNextThought())
					{
						this.addThought(tt.getNextThought());
					}
	            }
	        }
		}
		catch(InvalidOperationException e)
		{
			
		}
    }

    public double getAnxiousness()
    {
        return this.thoughts.Count;
    }
}
