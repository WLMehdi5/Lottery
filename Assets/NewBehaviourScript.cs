using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Text left;
    [SerializeField]
    private Text center;
    [SerializeField]
    private Text right;
    [SerializeField]
    private Button leftStopButton;
    [SerializeField]
    private Button centerStopButton;
    [SerializeField]
    private Button rightStopButton;
    [SerializeField]
    private Button startButton;

    bool leftStop = false;
    bool centerStop = false;
    bool rightStop = false;

    const int MAX_NUMBER = 200;

    System.Random r;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random((int)DateTime.Now.Ticks);

        leftStopButton.onClick.AddListener(
            () =>
            {
                leftStop = true;
            }
            );
        centerStopButton.onClick.AddListener(
            () =>
            {
                centerStop = true;
            }
            );
         rightStopButton.onClick.AddListener(
            () =>
            {
                rightStop = true;
            }
            );
        startButton.onClick.AddListener(
            () =>
            {
                leftStop = false;
                centerStop = false;
                rightStop = false;
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftStop)
        {
            left.text = r.Next(MAX_NUMBER).ToString();
        }
        if (!centerStop)
        {
            center.text = r.Next(MAX_NUMBER).ToString();
        }
        if (!rightStop)
        {
            right.text = r.Next(MAX_NUMBER).ToString();
        }
    }
}
