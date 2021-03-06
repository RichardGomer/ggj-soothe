﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Clickable : MonoBehaviour {

	public Clickable()
	{
		this.clicks = new RcvrHandler ();
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
		try
		{
        	return v.GetComponent<Clickable>();
		}
		catch(NullReferenceException e)
		{
			Debug.LogError(v.name + " is not Clickable!");
			throw e;
		}
    }
	
	/**
	 * Listen for clicks
	 */
    void onMouseDown()
    {
		// MouseDown doesn't work. It is supposed to, though.  FFS Unity.
        //this.click();
    }

	public void Update()
	{
		
		if (mouseWentDown ()) {
			this.click ();
		}

	}

	private bool mousedown = false;
	protected bool mouseWentDown()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll (ray, Mathf.Infinity);

			if(hits.Length < 1)
				return false;

			// Find the collision object with the highest z index
			// NB: Since we're using 2D physics, this isn't the highest collision per se,
			// rather the 2D object with the highest z transform (actually, most -ve)
			RaycastHit2D hit = hits[0];
			foreach(RaycastHit2D h in hits)
			{
				if(h.transform.position.z < hit.transform.position.z)
					hit = h;
			}

			if (hit.collider != null && hit.collider.transform == this.gameObject.transform)
			{
				this.mousedown = true;
				return true;
			}
		}

		return false;
	}

	protected bool mouseIsDown()
	{
		if (Input.GetMouseButtonUp (0)) {
			this.mousedown = false;
		}

		return this.mousedown;
	}
	
    /**
     * Subscribe to click events
     */
	RcvrHandler clicks;

    public void onClick(ClickRcvr c)
    {
		this.clicks.add (c);
    }
	
    /**
     * Trigger a click event
     */
    public void click()
    {
		this.clicks.trigger ();
    }

	public virtual void unsubscribe(object o)
	{
		this.clicks.unsubscribe (o);
	}

}

/**
 * ClickRcvr is just a callback signature
 */
public delegate void ClickRcvr();

/**
 * Keep a collection of ClickRvrs so we can support multiple events
 */
public class RcvrHandler
{
	protected List<ClickRcvr> receivers;

	public RcvrHandler()
	{
		this.receivers = new List<ClickRcvr> ();
	}

	public void add(ClickRcvr c)
	{
		this.receivers.Add(c);
		Debug.Log ("Handler was registered");
	}

	/**
     * Remove any click delegates that refer to the given object
     * Use when a handler goes away
     */
	public void unsubscribe(object o)
	{
		foreach (ClickRcvr cr in this.receivers)
		{
			if(cr.Target == o)
			{
				this.receivers.Remove(cr);
			}
		}
	}

	public void trigger()
	{
		foreach(ClickRcvr cr in this.receivers)
		{
			Debug.Log (" - Pass to handler");
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

