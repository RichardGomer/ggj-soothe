using UnityEngine;
using System.Collections;

public class SecurityAnxiety : CountAnxiety, Thought {

	public SecurityAnxiety(Clickable target, int required) : base(target, required)
	{

	}

	public override string getDescription()
	{
		return SpeechStrings.ANX_SECURITY;
	}
	
	public override string getCompletionSpeech()
	{
		return SpeechStrings.ANX_SECURITY_DONE;
	}
}
