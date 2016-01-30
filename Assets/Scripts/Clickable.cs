using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Clickable : MonoBehaviour {

	public Clickable()
	{
		this.clickReceivers = new List<ClickRcvr>();
	}

	// Use this for initialization
	void Start () {
        if(!this.gameObject.GetComponent<Collider2D>())
        {
            throw new MustBeColliderException("Clickable must be used on a game object with a Collider2D element!");
        }
	}
	

	// Conversion
    public static explicit operator Clickable(GameObject v)
    {
        return v.GetComponent<Clickable>();
    }
	
	/**
	 * Listen for clicks
	 */
    void onMouseDown()
    {
		// MouseDown doesn't work. It is supposed to, though.  FFS Unity.
        //this.click();
    }

	void Update()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			
			if(hit.collider != null && hit.collider.transform == this.gameObject.transform)
			{
				this.click ();
			}
		}
	}
	
    /**
     * Subscribe to click events
     */
	protected List<ClickRcvr> clickReceivers;
    public void onClick(ClickRcvr c)
    {
        this.clickReceivers.Add(c);
		print ("Click handler was registered");
    }
	
    /**
     * Trigger a click event
     */
    public void click()
    {
		print ("Click triggered");
        foreach(ClickRcvr cr in this.clickReceivers)
        {
			print (" - Pass to handler");
            cr();
        }
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

}

public class MustBeColliderException : Exception
{
    public MustBeColliderException(string message) : base(message)
    {
    }
}

public delegate void ClickRcvr();