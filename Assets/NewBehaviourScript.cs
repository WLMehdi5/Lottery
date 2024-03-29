﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Text left;
    [SerializeField]
    private Text center;
    [SerializeField]
    private Text right;

    [SerializeField]
    private Text hurigana;

    /*[SerializeField]
    private Button leftStopButton;
    [SerializeField]
    private Button centerStopButton;
    [SerializeField]
    private Button rightStopButton;
    [SerializeField]
    private Button startButton;
    */
    [SerializeField]
    private AudioSource one;
    [SerializeField]
    private AudioSource end;

    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private GameObject scrollView;
    [SerializeField]
    private GameObject scrollBar;
    [SerializeField]
    private GameObject slot;



    bool leftStop = true;
    bool centerStop = true;
    bool rightStop = true;
    bool birthDayMode = false;
    private List<string[]> csvList = null;

    private List<string> part = null;
    private List<string> section = null;
    private List<string> name = null;
    private List<string> huriganaList = null;

    const int MAX_NUMBER = 200;

    int selecter = -1;
  

    System.Random r;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random((int)DateTime.Now.Ticks);

        leftStop = true;
        centerStop = true;
        rightStop = true;
        birthDayMode = false;

        csvReader();
        /*
        leftStopButton.onClick.AddListener(
            () =>
            {
                leftClick();
            }
            );
        centerStopButton.onClick.AddListener(
            () =>
            {
                centerClick();             
            }
            );
         rightStopButton.onClick.AddListener(
            () =>
            {
                rightClick();
            }
            );
        startButton.onClick.AddListener(
            () =>
            {
                reset();

            }
        );
        */
    }

    // Update is called once per frame
    void Update()
    {
        // スロット化
        if (!leftStop)
        {
            left.text = part[r.Next(part.Count)];//csvList[0][r.Next(csvList[0].Length)];  //r.Next(MAX_NUMBER).ToString();
        }
        if (!centerStop)
        {
            center.text = section[r.Next(section.Count)]; //csvList[1][r.Next(csvList[1].Length)];//r.Next(MAX_NUMBER).ToString();
        }
        if (!rightStop)
        {
            right.text = name[r.Next(name.Count)];  //csvList[2][r.Next(csvList[2].Length)];// r.Next(MAX_NUMBER).ToString();
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            reset();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftClick();
            if (birthDayMode)
            {
                left.text = "社長！";
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            centerClick();
            if (birthDayMode)
            {
                center.text = "誕生日";
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rightClick();
            if (birthDayMode)
            {
                right.text = "おめでとうございます！！！";
            }
        }

        if (Input.GetKeyDown(KeyCode.B) )
        {
            if (leftStop && centerStop && rightStop)
            {
                birthDayMode = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (leftStop && centerStop && rightStop)
            {
                birthDayMode = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            scrollView.SetActive(!scrollView.activeSelf);
            scrollBar.SetActive(scrollView.activeSelf);
            slot.SetActive(!scrollView.activeSelf);
              
        }


    }

    public void csvReader()
    {
        // 読み込みたいCSVファイルのパスを指定して開く
        if (csvList != null)
        {
            return;
        }
        StreamReader sr = new StreamReader(@"name.csv");
        csvList = new List<string[]>();
        part = new List<string>();
        section = new List<string>();
        name = new List<string>();
        huriganaList = new List<String>();
        int i = 0;
        // 末尾まで繰り返す
        while (!sr.EndOfStream)
        {
            // CSVファイルの一行を読み込む
            string line = sr.ReadLine();
            // 読み込んだ一行をカンマ毎に分けて配列に格納する
            string[] values = line.Split(',');
            part.Add(values[0]);
            section.Add(values[1]);
            name.Add(values[2]);
            huriganaList.Add(values[3]);
            i++;
            // 配列からリストに格納する
            //csvList.Add(values);//line);
        }
        //return lists;
        //reset();

    }

    public void seSelect()
    {
        if (leftStop && centerStop && rightStop)
        {
            if (!birthDayMode)
            {
                hurigana.text = huriganaList[selecter];
                huriganaList.RemoveAt(selecter);
            }
            end.Play();
            
            return;
        }
        one.Play();
    }

    public void reset()
    {
        if (leftStop && centerStop && rightStop)
        {
            if (!birthDayMode)
            {
                hurigana.text = "";
                selecter = r.Next(part.Count);
                GameObject text = (GameObject)Resources.Load("history");
                text.GetComponent<Text>().text = part[selecter] + " " + section[selecter] + " " + name[selecter] + " "+ huriganaList[selecter];
                Instantiate(text, content.transform);
            }
            //text.GetComponent<RectTransform>().SetParent(content);

            
            //content.AddComponent<Text>();
            //content.GetComponent<Text>().text = part[selecter];


           
            leftStop = false;
            centerStop = false;
            rightStop = false;
        }
    }

    public void leftClick()
    {
        if (!leftStop)
        {
            leftStop = true;
            left.text = part[selecter];
            part.RemoveAt(selecter);
            seSelect();
        }

    }

    public void centerClick()
    {
        if (!centerStop)
        {
            centerStop = true;
            center.text = section[selecter];
            section.RemoveAt(selecter);
            seSelect();
        }

    }

    public void rightClick()
    {
        if (!rightStop)
        {
            rightStop = true;
            right.text = name[selecter];
            name.RemoveAt(selecter);
            seSelect();
        }

    }
}
