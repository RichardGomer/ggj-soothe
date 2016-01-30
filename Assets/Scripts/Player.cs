using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

    protected ArrayList thoughts;

	// Use this for initialization
	void Start () {

        this.thoughts = new ArrayList();

        // Start with anxiety about the light switch
        this.addThought(new CountAnxiety((Clickable) GameObject.Find("Switch"), 4));

	}
	
	// Update is called once per frame
	void Update () {
        this.purgeThoughts();

        // Update global UI stuff from here?

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

		this.say (t.getDescription(), 1000);
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
	                this.say("OK...", 600);
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
