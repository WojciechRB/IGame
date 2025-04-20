using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject body;

    [SerializeField]
    private GameObject massFeature;

    [SerializeField]
    private GameObject velocityFeature;

    [SerializeField]
    private GameObject accelerationFeature;

    [SerializeField]
    private GameObject positionFeature;

    [SerializeField]
    private GameObject forceFeature;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (body)
        {
            Body b = body.GetComponent<Body>();
            string mass = b.mass.ToString();
            string velocity = Vec2Str(b.velocity);
            string acceleration = Vec2Str(b.acceleration);
            string position = Vec2Str(b.position);
            string force = Vec2Str(b.force);

            if (massFeature)
                massFeature.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = mass;
            if (velocityFeature)
                velocityFeature.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = velocity;
            if (accelerationFeature)
                accelerationFeature.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = acceleration;
            if (positionFeature)
                positionFeature.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = position;
            if (forceFeature)
                forceFeature.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = force;
        }
    }

    private string Vec2Str(Vector2 v) => ("X: "+ Math.Round(v.x,2).ToString() + " Y: " + Math.Round(v.y, 2).ToString());
}
