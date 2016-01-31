using UnityEngine;
using System.Collections;
using System;

public interface Thought
{
     string getDescription();

	 string getCompletionSpeech();

     int getUrgency();

	 string getSound();

	 bool isComplete();

	 Thought getNextThought();

	 bool hasNextThought();
}
