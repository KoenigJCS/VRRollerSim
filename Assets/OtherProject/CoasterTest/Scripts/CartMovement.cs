//CS484
//Vladislav Petrov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CartMovement : MonoBehaviour
{
    public float pitch = 0.0f;
    public float speed = 0.0f;
    public float acceleration = 0.0f;
    public SplineFollower trackFollower;
    public AudioSource movementSound;
    public AudioSource chainSound;
    public List<GameObject> train;
    public GameObject mainCart;
    public List<Camera> CartCams;
    public int cameraIndex = 0;
    public bool freeTrack = true;
    public bool liftTrack = false;
    public bool stationTrack = false;
    public Color cartColor;
    public float cartOffset = 0.0f;

    public float cameraRotationX = 0.0f;
    public float cameraRotationY = 0.0f;
    public float cameraSensitivity = 1.0f;
    public float seatSide = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize coaster train
        //Cart positions
        for(var i = train.Count - 1; i >= 0; i--)
        {
            trackFollower = train[i].GetComponent<SplineFollower>();
            trackFollower.SetPercent(cartOffset);
            cartOffset += (0.75f / trackFollower.spline.CalculateLength());
        }

        cartColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        foreach (GameObject cart in train)
        {
            //Assign cameras
            CartCams.Add(cart.GetComponentInChildren<Camera>(true));
            cart.GetComponentInChildren<Camera>(true).enabled = false;
            cart.GetComponentInChildren<Camera>(true).GetComponent<AudioListener>().enabled = false;

            foreach (Renderer renderer in cart.GetComponentsInChildren<Renderer>())
            {
                if(renderer.gameObject.name == "Body")
                {
                    renderer.material.color = cartColor;
                }
            }
        }
        train[0].GetComponentInChildren<Camera>(true).enabled = true;
        train[0].GetComponentInChildren<Camera>(true).GetComponent<AudioListener>().enabled = true;


        //Find the middle cart. This is for better physics.
        mainCart = train[train.Count / 2];
    }

    // Update is called once per frame
    void Update()
    {
        //Select cart camera with arrow keys
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CartCams[cameraIndex].enabled = false;
            CartCams[cameraIndex].GetComponent<AudioListener>().enabled = false;
            if (cameraIndex < CartCams.Count - 1)
            {
                cameraIndex++;
            }
            CartCams[cameraIndex].enabled = true;
            CartCams[cameraIndex].GetComponent<AudioListener>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            CartCams[cameraIndex].enabled = false;
            CartCams[cameraIndex].GetComponent<AudioListener>().enabled = false;
            if (cameraIndex > 0)
            {
                cameraIndex--;
            }
            CartCams[cameraIndex].enabled = true;
            CartCams[cameraIndex].GetComponent<AudioListener>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            seatSide = -0.5f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            seatSide = 0.5f;
        }

        //Camera control (Using mouse. Needs VR support.)
        cameraRotationX += Input.GetAxis("Mouse Y") * -cameraSensitivity;
        cameraRotationY += Input.GetAxis("Mouse X") * cameraSensitivity;
        CartCams[cameraIndex].transform.localEulerAngles = new Vector3(cameraRotationX, cameraRotationY, 0);
        CartCams[cameraIndex].transform.localPosition = new Vector3(seatSide, 2, 0);


        //Figure out the acceleration and speed based on the angle when on free track
        if (freeTrack)
        {
            //Pitch Down
            if(mainCart.transform.localEulerAngles.x < 180)
            {
                pitch = mainCart.transform.localEulerAngles.x / 2500;
            }
            //Pitch Up
            else
            {
                pitch = (mainCart.transform.localEulerAngles.x - 360) / 2500;
            }
            acceleration = pitch;
            speed += acceleration;
        }

        //Lift hill
        if(liftTrack)
        {
            if(speed < 2)
            {
                speed += 0.025f;
            }
            if(speed > 2)
            {
                speed -= 0.025f;
            }
            acceleration = 0;
        }

        //Apply same speed and direction to all carts in the train.
        foreach (GameObject cart in train)
        {
            trackFollower = cart.GetComponent<SplineFollower>();
            if (speed >= 0)
            {
                trackFollower.direction = Spline.Direction.Forward;
            }
            else
            {
                trackFollower.direction = Spline.Direction.Backward;
            }
            trackFollower.followSpeed = speed;
        }

        //Sound effetcs
        movementSound.pitch = Mathf.Abs(speed) / 10;
        chainSound.pitch = Mathf.Abs(speed) / 2;
    }


    //Triggers
    void freeTrackFunction()
    {
        freeTrack = true;
        liftTrack = false;
        stationTrack = false;
        chainSound.Stop();
    }

    void liftTrackFunction()
    {
        freeTrack = false;
        liftTrack = true;
        stationTrack = false;
        chainSound.Play();
    }

    void stationTrackFunction()
    {

    }
}


