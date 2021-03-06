﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public GameObject gameOverImage;
    public GameObject startButton;
    public Text Score;

    [SerializeField]
    GameObject cat; // for reading cat object
    GameObject arrow;

    [SerializeField]
    float maxPower; // max power
    [SerializeField]
    float minPower; // min power
    [SerializeField]
    float lerpDuration; // power bar fill duration

    float power;
    float timeElapsed;

    [SerializeField]
    AnimationCurve powerCurve;

    [SerializeField]
    Slider powerBar;

    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        powerBar.minValue = minPower;
        powerBar.maxValue = maxPower;

        arrow = cat.transform.Find("arrow").gameObject; // get arrow child to check if it's active (instead of using sendMessage cause I'm lazy)
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 catScreenPosition = Camera.main.WorldToScreenPoint(cat.transform.position); // get cat's screen position

        if(catScreenPosition.y < -50 && !dead) // if cat is off the screen
        {
            dead = true;

            gameOverImage.SetActive(true);
            startButton.SetActive(true);

            Score.text = "SCORE: " + (int)Camera.main.transform.position.y + "m";
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get mouse position in world space

        if (arrow.activeSelf && !dead)
        {

            if (Input.GetMouseButton(0)) // when mouse is held
            {
                timeElapsed += Time.deltaTime; // inscrease timeElapsed
                power = Mathf.Lerp(minPower, maxPower, powerCurve.Evaluate(timeElapsed / lerpDuration)); // lerp power with AnimationCurve
                powerBar.value = power; // update powerBar
            }

            if (Input.GetMouseButtonUp(0)) // when mouse is released
            {
                Vector2 direction = mousePosition - cat.transform.position; // get direction from cat to mouse position 
                direction.Normalize(); // normalize it so it's a real direction vector
                direction *= power; // set it's magnitude to power level

                cat.SendMessage("Launch", direction); // send force to cat

                power = minPower;
                timeElapsed = 0;
                powerBar.value = power;
            }
        }

    }
}
