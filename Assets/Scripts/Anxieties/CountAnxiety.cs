using UnityEngine;
using System.Collections;

public class CountAnxiety : Anxiety, Thought
{
    protected int count = 0;
    protected int required = 0;
    protected Clickable target;

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

    // Also destabilise when progress is made
    protected int lastProgress;
    public override string getStableDescription()
    {
        if (this.lastProgress != this.getCount() || this.lastString == "" || this.getUrgency() != this.lastUrgency || this.lastTime < Time.time - 10)
        {
            this.lastString = this.getDescription();
            this.lastProgress = this.getCount();
            this.lastUrgency = this.getUrgency();
            this.lastTime = (int) Time.time;
        }

        return this.lastString;
    }

    public void incrementCount()
    {
        this.count++;
    }

    public int getCount()
    {
        return this.count;
    }

	public override bool isComplete()
    {
        return this.count > 0 && (this.count % this.required == 0);
    }
}
