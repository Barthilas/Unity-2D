using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 1;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;
    //or public


    SurfaceEffector2D[] surfaceEffector2;
    bool canMove = true;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2 = FindObjectsOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            ReactToBoost();
            RotatePlayer();
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    private void ReactToBoost()
    {
        var surfaceList = surfaceEffector2.ToList();
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            surfaceList.ForEach(surface => surface.speed = boostSpeed);
        }
        else
        {
            surfaceList.ForEach(surface => surface.speed = baseSpeed);
        }
    }

    private void RotatePlayer()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}