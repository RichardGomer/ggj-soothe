using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
	
	public Text textEl;
	// Use this for initialization
	void Start () {
		this.textQueue = new List<SpeechEntry>();
        this.queueText("<Initialised Speech>", 1000);
	}

    // Update is called once per frame
    private int now; // Time since start of game in ms
    void Update () {
        this.now = System.Convert.ToInt32( Time.time * 1000 );
	}

    
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

    string textBuffer;
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
}

public class SpeechEntry
{
	private string text;
	private int duration;
	private int start;

	public SpeechEntry(string text, int duration)
	{
		this.text = text;
		this.duration = duration;
	}

	public string getText()
	{
		return this.text;
	}

	public void setStart(int start)
	{
		this.start = start;
	}

	public int getStart()
	{
		return this.start;
	}

	public int getEnd()
	{
		return this.start + this.duration;
	}
}
