using UnityEngine;
using System.Collections;

public class LightAnxiety : CountAnxiety, Thought {

	public LightAnxiety(Clickable target, int required) : base(target, required)
	{
	}

	public override string getDescription()
	{
		return SpeechStrings.ANX_LIGHT;
	}
	
	public override string getCompletionSpeech()
	{
		return SpeechStrings.ANX_LIGHT_DONE;
	}
}
