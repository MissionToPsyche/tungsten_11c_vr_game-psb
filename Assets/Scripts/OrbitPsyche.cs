// Created 1/21/24 - Peyton O'Boyle
// Handles launch and gravity's effect
// Modification History:
/* 
    1/21/24 - Peyton O'Boyle

    1/23/24 - Peyton O'Boyle

    1/24/24 - Peyton O'Boyle
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPsyche : MonoBehaviour
{
    public GameObject startPoint;

    public GameObject asteroid;

    public GameObject equation;

    public Color[] colors;

    public GameObject trailCreator;
    private GameObject[] trails;
    private int trailIndex;

    public GameObject slide;

    //Masses in kg
    public float satelliteMass;
    public float baseAsteroidMass;
    //The mass to be added to the asteroid from the slider
    public float addedAsteroidMass;
    //baseAsteroidMass with mass from slider applied
    private float currentAsteroidMass;

    //The gravitational constant
    private double gravitationalConstant = 6.674e-11f;

    //Distance in m
    //Distance is 190 km (190000m) in Orbit C
    //Distance multiplier * (Satellite y pos - Psyche y pos) = 190000
    private int distanceMultiplier = 38000;

    public float orientSpeed;
    private bool rotationEnabled;

    private bool gravity;
    private bool timerStarted;
    private bool launched;

    //Ensure the narration isn't repeated
    private bool firstButton;
    private bool firstGravityIncrease;
    private bool firstGravityDecrease;

    // Start is called before the first frame update
    void Start()
    {
        //Set the position of the satellite to the startPoint
        this.transform.position = startPoint.transform.position;

        //Set trail index to 0
        trailIndex = -1;
        trails = new GameObject[3];

        //Set gravity to false
        gravity = false;

        //Set timerStarted to false
        timerStarted = false;

        launched = false;

        //Set the narration to activate once
        firstButton = true;
        firstGravityIncrease = true;
        firstGravityDecrease = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If gravity is true, apply it
        if (gravity == true)
        {
            //Add a vector to that vector that represents the gravitational pull of the asteroid
            Vector3 gravDir = this.transform.position - asteroid.transform.position;

            double gravitationalForce = gravitationalConstant * (satelliteMass * currentAsteroidMass) / Mathf.Pow(Vector3.Distance(this.transform.position, asteroid.transform.position) * distanceMultiplier, 2);

            GetComponent<Rigidbody>().AddForce(-gravDir.normalized * (float)gravitationalForce * 0.0015f, ForceMode.Impulse);

            if (rotationEnabled == true)
            {
                Rotate(-gravDir);
            }
            else if (this.transform.position.x > asteroid.transform.position.x)
            {
                rotationEnabled = true;
            }
        }

        //Update the distance
        equation.GetComponent<Equation>().setDistance(Vector3.Distance(this.transform.position, asteroid.transform.position) * distanceMultiplier);

        if (timerStarted == false && gravity == true)
        {
            timerStarted = true;
            StartCoroutine(WaitForReset(10));
        }
    }

    private IEnumerator WaitForReset(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //If gravity is true (i.e. the satellite has not been reset), reset
        if (gravity == true)
        {
            Reset();
        }
    }

    //If the satellite hits the planet, reset its position
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Psyche")
        {
            Reset();
        }
    }

    public void Launch()
    {
        if (launched == true)
        {
            return;
        }

        launched = true;

        //Create a new trail GameObject
        GameObject newTrail = Instantiate(trailCreator, transform.position, transform.rotation);

        //If this is number 4 of the trails, delete the first item in the list
        if (trailIndex > 1)
        {
            trailIndex = -1;
        }

        //Set the color
        newTrail.GetComponent<TrailRenderer>().material.color = colors[trailIndex + 1];

        trailIndex++;

        //Add that to the list of trails
        Destroy(trails[trailIndex]);
        trails[trailIndex] = newTrail;

        //Have it follow the satelitte
        newTrail.transform.SetParent(this.transform);

        //Ask the slider for the force of gravity
        float sliderForce = slide.GetComponent<GetLevel>().getLevel();

        //Create the current asteroid mass for this run
        currentAsteroidMass = baseAsteroidMass + sliderForce * addedAsteroidMass;

        //Set the asteroid mass in equation
        equation.GetComponent<Equation>().setAsteroidMass(currentAsteroidMass);

        //Set the equation to display numbers
        equation.GetComponent<Equation>().setDisplayMode(1);

        //Create vector that is moving from left to right 
        Vector3 startForce = new Vector3(5, 0, 0);

        //Add that force
        GetComponent<Rigidbody>().AddForce(startForce, ForceMode.VelocityChange);

        //Turn gravity on
        gravity = true;
    }

    void Reset()
    {
        //Set gravity to false
        gravity = false;

        //Set timerStarted to false
        timerStarted = false;

        //Disconnect current trail
        trails[trailIndex].transform.SetParent(null);

        //Set velocity to 0
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Reset rotation
        transform.rotation = Quaternion.identity;
        rotationEnabled = false;

        //Set the position of the satellite to the startPoint
        this.transform.position = startPoint.transform.position;

        //Set the equation to display variables
        equation.GetComponent<Equation>().setDisplayMode(0);

        //Activate the text
        //First time the button is pressed, play button press message
        if (firstButton == true)
        {
            SelectText("ButtonPress");
        }

        //On subsequent presses, determine if gravity is high or low and use the right dialouge
        else
        {
            if (currentAsteroidMass > baseAsteroidMass)
            {
                SelectText("GravityIncrease");
            }
            else if (currentAsteroidMass < baseAsteroidMass)
            {
                SelectText("GravityDecrease");
            }
        }

        launched = false;
    }

    void Rotate(Vector3 down)
    {
        //
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, orientSpeed * Time.deltaTime);
    }

    void SelectText(string selection)
    {
        //Get the manager
        GravityScienceTextManager textManager = GameObject.Find("GravityScienceTextManager").GetComponent<GravityScienceTextManager>();

        //Select the correct text
        //Button Press
        if (selection == "ButtonPress" && firstButton == true)
        {
            firstButton = false;

            textManager.SelectNarration("ButtonPress");
        }

        //Gravity Increase
        else if (selection == "GravityIncrease" && firstGravityIncrease == true)
        {
            firstGravityIncrease = false;
            
            textManager.SelectNarration("GravityIncrease");
        }

        //Gravity Decrease
        else if (selection == "GravityDecrease" && firstGravityDecrease == true)
        {
            firstGravityDecrease = false;

            textManager.SelectNarration("GravityDecrease");
        }
    }
}