using System;
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
    private Button leftStopButton;
    [SerializeField]
    private Button centerStopButton;
    [SerializeField]
    private Button rightStopButton;
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private AudioSource one;
    [SerializeField]
    private AudioSource end;


    bool leftStop = true;
    bool centerStop = true;
    bool rightStop = true;
    bool birthDayMode = false;
    private List<string[]> csvList = null;

    private List<string> part = null;
    private List<string> section = null;
    private List<string> name = null;

    const int MAX_NUMBER = 200;

    int selecter = -1;
  

    System.Random r;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random((int)DateTime.Now.Ticks);
        csvReader();

        leftStop = true;
        centerStop = true;
        rightStop = true;
        birthDayMode = false;
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
            i++;
            // 配列からリストに格納する
            //csvList.Add(values);//line);
        }
        //return lists;
        reset();

    }

    public void seSelect()
    {
        if(leftStop && centerStop && rightStop)
        {
            end.Play();
            
            return;
        }
        one.Play();
    }

    public void reset()
    {
        if (leftStop && centerStop && rightStop)
        {
            selecter = r.Next(part.Count);
            leftStop = false;
            centerStop = false;
            rightStop = false;
        }
    }

    public void leftClick()
    {
        leftStop = true;
        left.text = part[selecter];
        part.RemoveAt(selecter);
        seSelect();

    }

    public void centerClick()
    {
        centerStop = true;
        center.text = section[selecter];
        section.RemoveAt(selecter);
        seSelect();

    }

    public void rightClick()
    {
        rightStop = true;
        right.text = name[selecter];
        name.RemoveAt(selecter);
        seSelect();

    }
}
