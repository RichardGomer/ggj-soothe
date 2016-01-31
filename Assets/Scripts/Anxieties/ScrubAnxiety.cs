using UnityEngine;
using System.Collections;

public class ScrubAnxiety : CountAnxiety, Thought {

	public ScrubAnxiety(Sink target, int required) : base(required)
	{
		// Listen for scrubs instead of clicks
		ClickRcvr cr = this.incrementCount;
		target.onScrub (cr);
	}
	

    public override string getDescription()
    {
        if (this.getCount() > 1)
        {
            return this.pickRandomString(SpeechStrings.ANX_CLEAN_PROGRESSING);
        }

        switch (this.getUrgency())
        {
            case 0:
                return this.pickRandomString(SpeechStrings.ANX_CLEAN_MILD);
                break;
            case 1:
                return this.pickRandomString(SpeechStrings.ANX_CLEAN_MEDIUM);
                break;
            case 2:
                return this.pickRandomString(SpeechStrings.ANX_CLEAN_MAJOR);
                break;
        }

        return "???";
    }
	
	public override string getCompletionSpeech()
	{
		return SpeechStrings.ANX_CLEAN_DONE;
	}
}
