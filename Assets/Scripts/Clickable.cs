using UnityEngine;
using System.Collections;
using System;

public class Clickable : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.clickReceivers = new ArrayList();

        if(!this.gameObject.GetComponent<Collider2D>())
        {
            throw new MustBeColliderException("Clickable must be used on a game object with a Collider2D element!");
        }
        
	}

    // List of delegates to trigger each time we're clicked
    protected ArrayList clickReceivers;

    public void onMouseDown()
    {
        this.click();
    }

    /**
     * Subscribe to click events
     */
    public void onClick(ClickRcvr c)
    {
        this.clickReceivers.Add(c);
    }

    /**
     * Remove any click delegates that refer to the given object
     * Use when a handler goes away
     */
    public void unsubscribe(object o)
    {
        foreach (ClickRcvr cr in this.clickReceivers)
        {
            if(cr.Target == o)
            {
                this.clickReceivers.Remove(cr);
            }
        }
    }

    /**
     * Trigger a click event
     */
    public void click()
    {
        foreach(ClickRcvr cr in this.clickReceivers)
        {
            cr();
        }
    }
}

public class MustBeColliderException : Exception
{
    public MustBeColliderException(string message) : base(message)
    {
    }
}

public delegate void ClickRcvr();