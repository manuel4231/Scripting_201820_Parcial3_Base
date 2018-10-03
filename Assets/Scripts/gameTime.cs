using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTime : MonoBehaviour {

    [SerializeField]
    private Text timerText;
    public float t;
    private float startTime;

    // Use this for initialization
    void Start () {

        if (timerText == null)
        {
            enabled = false;
        }
        startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {

        t = Time.time - startTime;

        timerText.text = t.ToString();
        //TODO: Set text from GameController

        if (t >= 25)
        {
            Debug.Log("Game Over");

        }

    }
}
