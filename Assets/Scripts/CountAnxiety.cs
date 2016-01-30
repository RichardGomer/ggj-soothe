using UnityEngine;
using System.Collections;

public class CountAnxiety : Anxiety
{
    private int count = 0;
    private int required = 0;

    public CountAnxiety(Clickable target, int required)
    {
        ClickRcvr rcv = this.incrementCount;
        target.onClick(rcv);
        this.required = required;
    }

    public void incrementCount()
    {
        this.count++;
    }

    new public bool isComplete()
    {
        return (this.count % this.required == 0);
    }
	
}
