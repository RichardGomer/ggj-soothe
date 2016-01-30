using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Speech : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.textQueue = new Dictionary<int, string>();
        this.queueText("<Initialised Speech>", 1000);
	}

    // Update is called once per frame
    private int now; // Time since start of game in ms
    void Update () {
        this.now = System.Convert.ToInt32( Time.time * 1000 );
	}

    

    private Dictionary<int, string> textQueue;
    private int lastdelay = 0;
    public void queueText(string text, int delay)
    {
        // Calculate the end time of the last entry
        int last = 0;
        if (textQueue.Count > 0)
            last = textQueue.Keys.Max();

        int from = last + this.lastdelay;

        // If that's before now, then use now
        if(from < now)
            from = this.now;

        // Store the delay on this text to add to the start time of the next element
        this.lastdelay = delay;

        this.textQueue.Add(from, text);
    }

    string textBuffer;
    protected string getCurrentText()
    {
        string answer = "";
        int last = -1;

        foreach(int startTime in this.textQueue.Keys)
        {
            if(startTime < this.now)
            {
                answer = this.textQueue[startTime];

                // We've found newer text, so remove the old entry
                if(last > 0)
                {
                    this.textQueue.Remove(last);
                }
            } 
            // Stop when we reach text for the future
            else if(startTime > now)
            {
                break;
            }
        }

        return answer;
    }

    public void OnGUI()
    {
        this.now = System.Convert.ToInt32(Time.time * 1000);


        // Print current text
        Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Label(textArea, this.getCurrentText());
    }
}
