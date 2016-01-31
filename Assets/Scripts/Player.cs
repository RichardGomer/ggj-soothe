using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    protected ArrayList thoughts;
	public UIFacade UIF;
	public Clock clock;
    public UnityEngine.Audio.AudioMixer audioMixer;

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
		this.clock.reset ();

		Debug.Log ("Setup level " + level);

        Anxiety a1, a2, a3; // Helpful in a sec xD

        GameObject wi = GameObject.Find("WashInstructions");
        Vector3 wipos = wi.GetComponent<Transform>().position;

        switch (level % 5) {

			case 4:
                this.addThought(a1 = new LightAnxiety((Clickable)GameObject.Find("Switch"), 4));
                a1.chainThought(new ScrubAnxiety((Sink)GameObject.Find("Sink"), 2));

                this.addThought(new SecurityAnxiety());

                return;

			case 3:
				this.addThought(new SecurityAnxiety());
                return;

            case 2:
				this.addThought(a1 = new LightAnxiety((Clickable) GameObject.Find("Switch"), 6));
				a1.chainThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));

                wi.GetComponent<Transform>().position = new Vector3(wipos.x, wipos.y, 600);
                return;


			case 1:
				this.addThought(new ScrubAnxiety((Sink) GameObject.Find("Sink"), 2));
                wi.GetComponent<Transform>().position = new Vector3(wipos.x, wipos.y, 0);
                return;


			case 0:
				this.addThought(new LightAnxiety((Clickable) GameObject.Find("Switch"), 6));
                wi.GetComponent<Transform>().position = new Vector3(wipos.x, wipos.y, 600);
                return;
		}

	}

    // Convenience methods that delegate to the speech system
    public void say(string s, int delay)
    {
        this.GetComponent<Speech>().queueText(s, delay);
    }

    public void think(Thought t)
    {
        this.GetComponent<Speech>().queueThought(t);
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

		this.think (t);
    }

    public void removeThought(Thought t)
    {
        this.thoughts.Remove(t);
    }

    // Clean up completed thoughts
    protected void purgeThoughts()
    {
		try
		{
            int maxUrgency = 0;

		    foreach(Thought tt in this.thoughts)
	        {
				if (tt.isComplete())
	            {
	                this.thoughts.Remove(tt);
	                this.say(tt.getCompletionSpeech(), 3000);

					// See if there are chained thoughts...
					if(tt.hasNextThought())
					{
						this.addThought(tt.getNextThought());
					}
	            }
                else
                {
                    maxUrgency = Math.Max(maxUrgency, tt.getUrgency());
                }
	        }

            // TODO: Cross fade...
            float volHigh = 0f;
            float volLow = -40f;

            if (maxUrgency > 1)
            {
                this.audioMixer.SetFloat("PanicVol", volHigh);
                this.audioMixer.SetFloat("RelaxVol", volLow);
            }
            else
            {
                this.audioMixer.SetFloat("PanicVol", volLow);
                this.audioMixer.SetFloat("RelaxVol", volHigh);
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
