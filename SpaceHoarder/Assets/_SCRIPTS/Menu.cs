﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Menu : MonoBehaviour
{
	[SerializeField] private string _sceneName;
	
	void Update ()
    {
		if (CrossPlatformInputManager.GetButton("Submit") || Input.anyKeyDown)
        {
            if (_sceneName != null)
            {
			    SceneManager.LoadScene(_sceneName);
                return;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
		}
	}
}