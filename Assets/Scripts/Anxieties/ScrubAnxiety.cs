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
		return SpeechStrings.ANX_CLEAN;
	}
	
	public override string getCompletionSpeech()
	{
		return SpeechStrings.ANX_CLEAN_DONE;
	}
}
