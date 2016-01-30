using UnityEngine;
using System.Collections;

public class CountAnxiety : Anxiety, Thought
{
    private int count = 0;
    private int required = 0;
	private Clickable target;

    public CountAnxiety(Clickable target, int required)
    {
		this.required = required;
		this.target = target;

		// Register for clicks
        ClickRcvr rcv = this.incrementCount;
        this.target.onClick(rcv);
    }

	public string getDescription()
    {
		return SpeechStrings.ANX_LIGHT;
    }

	public string getCompletionSpeech()
	{
		return SpeechStrings.ANX_LIGHT_DONE;
	}

    public void incrementCount()
    {
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
