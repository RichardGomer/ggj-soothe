using UnityEngine;
using System.Collections;
using System;

public abstract class Anxiety : Thought
{
    public string getSound()
    {
        throw new NotImplementedException();
    }

	public virtual string getDescription(){
		return "<NOT IMPLEMENTED>";
	}

	public virtual string getCompletionSpeech(){
		return "<NOT IMPLEMENTED>";
	}

	abstract public bool isComplete();

	public void chainThought(Thought t)
	{
		this.nextThought = t;
	}

	private Thought nextThought = null;
	public bool hasNextThought()
	{
		return this.nextThought != null;
	}
	
	public Thought getNextThought()
	{
		return this.nextThought;
	}
}
