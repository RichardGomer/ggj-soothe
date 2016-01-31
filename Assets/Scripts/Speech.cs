using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public class Speech : MonoBehaviour {
	
	public Canvas uiCanvas;
	// Use this for initialization
	void Start () {

		this.textQueue = new List<SpeechEntry>();

	}

    // Update is called once per frame
    private long now; // Time since start of game in ms
    public void Update()
    {
        // Update time
        this.now = System.Convert.ToInt64(Time.time * 1000);

        // Find any bubbles that have expired and remove them
        foreach (SpeechEntry se in this.textQueue)
        {
            // Update the text
            se.getBubble().setText(se.getText());

            // TODO: Update the animation

            if (se.hasExpired())
            {
                Debug.Log("Speech entry has expired");
                this.textQueue.Remove(se);
                Destroy(se.getBubble().gameObject);
            }
        }

    }


    private List<SpeechEntry> textQueue;
	public void queueText(string text, int delay)
	{
		SpeechEntry entry = new TimedSpeechEntry (text, delay);

        Debug.Log("Add text: " + text);

		this.textQueue.Add(entry);

        // Generate and show the bubble
        Bubble bubble = this.genBubble ();
		entry.setBubble (bubble);
	}

    public void queueThought(Thought thought)
    {
        SpeechEntry entry = new ThoughtSpeechEntry(thought);

        Debug.Log("Add thought: " + thought.getDescription());

        this.textQueue.Add(entry);

        // Generate and show the bubble
        Bubble bubble = this.genBubble();
        entry.setBubble(bubble);
    }

	public Bubble bubbleMaster;
	protected Bubble genBubble()
	{
		Rect rect = uiCanvas.pixelRect;
		System.Random rand = new System.Random ();

        int border = 100;
		int xpos = rand.Next (System.Convert.ToInt32( rect.width - 2 * border)) + border;
		int ypos = rand.Next (System.Convert.ToInt32( rect.height -2 * border)) + border;

		Vector3 pos = new Vector3 (xpos, ypos, -3);
		Bubble bclone = (Bubble) Instantiate (this.bubbleMaster, pos, Quaternion.identity);
        bclone.transform.SetParent(this.uiCanvas.transform, false);

        RectTransform rt = bclone.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);

		return bclone;
	}


}

abstract public class SpeechEntry
{
	private long start;
	private Bubble bubble = null;

	public SpeechEntry()
	{
		this.start = System.Convert.ToInt64(Time.time * 1000);
	}

    abstract public bool hasExpired();

    public abstract string getText();

    public long getStart()
	{
		return this.start;
	}

	public void setBubble(Bubble bubble)
	{
		this.bubble = bubble;
	}

	public Bubble getBubble()
	{
		return this.bubble;
	}
}

public class ThoughtSpeechEntry : SpeechEntry
{
    protected Thought thought;

    private int lastTextTime = -100;
    private string lastText = "";
    public ThoughtSpeechEntry(Thought thought)
    {
        this.thought = thought;
    }

    private int oldUrgency = -1;
    private string text = "";
    public override string getText()
    {
        return this.thought.getDescription();
    }

    override public bool hasExpired()
    {
        return this.thought.isComplete();
    }
}

public class TimedSpeechEntry : SpeechEntry
{
    protected int period;
    private string text;

    // Period is in ms!
    public TimedSpeechEntry(string text, int period)
    {
        this.period = period;
        this.text = text;
    }

    public override string getText()
    {
        return this.text;
    }

    public override bool hasExpired()
    {
        return this.getStart() + this.period < System.Convert.ToInt64(Time.time * 1000);
    }
}
