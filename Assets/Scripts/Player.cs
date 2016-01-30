using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected ArrayList thoughts;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        this.purgeThoughts();

        // Update global UI stuff from here?

    }

    public ArrayList getThoughts()
    {
        return this.thoughts;
    }
    
    public void addThought(Thought t)
    {
        this.thoughts.Add(t);
    }

    public void removeThought(Thought t)
    {
        this.thoughts.Remove(t);
    }

    /**
     * Clean up "complete" thoughts
     */
    protected void purgeThoughts()
    {
        foreach(Thought tt in this.thoughts)
        {
            if(tt.isComplete())
            {
                this.thoughts.Remove(tt);
            }
        }
    }

    public double getAnxiousness()
    {
        return this.thoughts.Count;
    }
}
