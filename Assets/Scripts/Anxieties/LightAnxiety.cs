using UnityEngine;
using System.Collections;

public class LightAnxiety : CountAnxiety, Thought {

	public LightAnxiety(Clickable target, int required) : base(target, required)
	{
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
		return SpeechStrings.ANX_LIGHT_DONE;
	}
}
