using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

    protected ArrayList thoughts;
	public UIFacade UIF;

	// Use this for initialization
	void Start () {

        this.thoughts = new ArrayList();

		this.reset (0);

	}
	
	// Update is called once per frame
	void Update () {
        this.purgeThoughts();

        // Update global UI stuff from here?


		// Level completes
		if (this.thoughts.Count < 1) {

			this.UIF.reset();

			this.reset (this.level+1);
		}
    }

	private int level;
	void reset(int level){

		this.level = level;

		switch (level) {
			case 4:

			case 3:

			case 2:
				this.addThought(new CountAnxiety((Clickable) GameObject.Find("Switch"), 4));
				this.addThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));
			break;

			case 1:
				this.addThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));
				
			break;

			case 0:
				this.addThought(new CountAnxiety((Clickable) GameObject.Find("Switch"), 4));

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
	                this.say("OK...", 1000);
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
