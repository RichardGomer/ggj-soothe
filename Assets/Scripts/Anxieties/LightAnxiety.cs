using UnityEngine;
using System.Collections;

public class LightAnxiety : CountAnxiety, Thought {

    public LightAnxiety(Clickable target, int required) : base(target, required)
	{
        this.target = target;
        this.required = required;
	}

	public override string getDescription()
	{
        if(this.getCount() > 2)
        {
            return this.pickRandomString(SpeechStrings.ANX_LIGHT_PROGRESSING);
        }

		switch(this.getUrgency())
        {
            case 0:
                return this.pickRandomString(SpeechStrings.ANX_LIGHT_MILD);
                break;
            case 1:
                return this.pickRandomString(SpeechStrings.ANX_LIGHT_MEDIUM);
                break;
            case 2:
                return this.pickRandomString(SpeechStrings.ANX_LIGHT_MAJOR);
                break;
        }

        return "???";
	}
	
	public override string getCompletionSpeech()
	{
        // Failure!
        // this is VERY brittle...
        // Call getCompletionSpeech() before hasNextThought() for failure to work...
        System.Random r = new System.Random();
        if (r.Next(10) > 7)
        {
            // FAIL
            this.chainThought(new LightAnxiety(this.target, this.required));

            return this.pickRandomString(SpeechStrings.ANX_LIGHT_FAIL);
        }

        return this.pickRandomString(SpeechStrings.ANX_LIGHT_DONE);
	}
}
