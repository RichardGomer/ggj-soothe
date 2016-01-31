using UnityEngine;
using System.Collections;
using System;

public class SecurityAnxiety : Anxiety, Thought {

    private GameObject window;
    private GameObject door;

    private bool window_done = false;
    private bool door_done = false;

	public SecurityAnxiety()
	{
        this.window = GameObject.Find("Window");
        this.door = GameObject.Find("FrontDoor");

        ClickRcvr cwin = this.click_window;
        ClickRcvr cdoor = this.click_door;

        this.window.GetComponent<Clickable>().onClick(cwin);
        this.door.GetComponent<Clickable>().onClick(cdoor);
    }

    public void click_window()
    {
        this.window_done = true;
    }

    public void click_door()
    {
        this.door_done = true;
    }

    public override bool isComplete()
    {
        return this.window_done && this.door_done;
    }

    public override string getDescription()
    {
        switch (this.getUrgency())
        {
            case 0:
                return this.pickRandomString(SpeechStrings.ANX_SECURITY_MILD);
                break;
            case 1:
                return this.pickRandomString(SpeechStrings.ANX_SECURITY_MEDIUM);
                break;
            case 2:
                return this.pickRandomString(SpeechStrings.ANX_SECURITY_MAJOR);
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
        if (r.Next(5) > 3)
        {
            // FAIL
            this.chainThought(new SecurityAnxiety());

            return this.pickRandomString(SpeechStrings.ANX_SECURITY_FAIL);
        }

        return SpeechStrings.ANX_SECURITY_DONE;
	}
}
