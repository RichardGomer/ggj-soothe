using UnityEngine;
using System.Collections;
using System;

public interface Thought
{
    string getDescription();

	string getCompletionSpeech();

    string getSound();

    bool isComplete();
}
