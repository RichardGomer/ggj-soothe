using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
	
	public Canvas uiCanvas;
	// Use this for initialization
	void Start () {

		this.bubbleMaster = (Bubble) Resources.Load ("AnxietyBubble");

		this.textQueue = new List<SpeechEntry>();
        this.queueText("", 1000);

	}

    // Update is called once per frame
    private long now; // Time since start of game in ms
    void Update () {
        this.now = System.Convert.ToInt64( Time.time * 1000 );
	}


	private List<SpeechEntry> textQueue;
	public void queueText(string text, int delay)
	{
		SpeechEntry entry = new SpeechEntry (text, delay);	

		this.textQueue.Add(entry);

		// Generate and show the bubble
		Bubble bubble = this.genBubble ();
		bubble.setText (text);
		entry.setBubble (bubble);
	}

	public Bubble bubbleMaster;
	protected Bubble genBubble()
	{
		Rect rect = uiCanvas.pixelRect;
		System.Random rand = new System.Random ();

		int xpos = rand.Next (System.Convert.ToInt32( rect.width));
		int ypos = rand.Next (System.Convert.ToInt32( rect.height));

		Vector3 pos = new Vector3 (xpos, ypos, -3);
		Bubble bclone = (Bubble) Instantiate (bubbleMaster, pos, Quaternion.identity);

		return bclone;
	}

	public void onGUI()
	{
		// Update time
		this.now = System.Convert.ToInt64(Time.time * 1000);

		// Find any bubbles that have expired and remove them
		foreach (SpeechEntry se in this.textQueue) {
			if(se.hasExpired())
			{
				this.textQueue.Remove(se);
				Destroy(se.getBubble().gameObject);
			}
		}

	}


    /*
    public Text textEl;
    private List<SpeechEntry> textQueue;
    private int currentEnd = 0;
    public void queueText(string text, int delay)
    {
		int start = this.currentEnd;

		if (start < now) {
			start = now;
		}

		SpeechEntry entry = new SpeechEntry (text, delay);
		entry.setStart (start);
		this.currentEnd = entry.getEnd ();

        this.textQueue.Add(entry);
    }
	
    protected string getCurrentText()
    {
        foreach(SpeechEntry entry in this.textQueue)
        {
            if(entry.getEnd() < now)
			{
				this.textQueue.Remove(entry);
			}
			else
			{
				return entry.getText();
			}
        }

		return "";
    }

    public void OnGUI()
    {
        this.now = System.Convert.ToInt32(Time.time * 1000);
		this.textEl.text = this.getCurrentText ();
    }
    */
}

public class SpeechEntry
{
	private string text;
	private int duration;
	private long start;
	private Bubble bubble = null;

	public SpeechEntry(string text, int duration)
	{
		this.text = text;
		this.duration = duration;
		this.start = System.Convert.ToInt64(Time.time * 1000);
	}

	public bool hasExpired()
	{
		// TODO! Link this back to a timer or the original thought?
		// via replacements to queueText; queueThought()?
		return false;
	}

	public long getStart()
	{
		return this.start;
	}


	public string getText()
	{
		return this.text;
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
