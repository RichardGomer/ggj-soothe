using UnityEngine;
using System.Collections;
using System;

public abstract class Anxiety : Thought
{
    private int start;
    public Anxiety()
    {
        this.start = System.Convert.ToInt32(Time.time);
    }

    protected int getAge()
    {
        return System.Convert.ToInt32(Time.time) - this.start;
    }

    public int getUrgency()
    {
        int age = this.getAge();

        if(age < 10)
        {
            return 0;
        }
        else if(age < 20)
        {
            return 1;
        }
        else 
        {
            return 2;
        }
        
    }

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

    // Pick a random string from the given ; separated strings
    protected virtual string pickRandomString(string master)
    {
        Char[] delims = { ';', ':' };
        String[] strings = master.Split(delims);

        System.Random rand = new System.Random();
        return strings[rand.Next(strings.Length-1)];
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
