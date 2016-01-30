using UnityEngine;
using System.Collections;

public class CountAnxiety : Anxiety, Thought
{
    private int count = 0;
    private int required = 0;
	private Clickable target;

	public CountAnxiety(int required)
	{
		this.required = required;
	}

    public CountAnxiety(Clickable target, int required)
    {
		this.required = required;

		// Register for clicks
        ClickRcvr rcv = this.incrementCount;
        target.onClick(rcv);
    }

    public void incrementCount()
    {
        this.count++;
    }

	public override bool isComplete()
    {
        return this.count > 0 && (this.count % this.required == 0);
    }
}
