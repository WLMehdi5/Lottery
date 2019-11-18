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

    bool leftStop = false;
    bool centerStop = false;
    bool rightStop = false;
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
        
        leftStopButton.onClick.AddListener(
            () =>
            {
                leftStop = true;
                left.text = part[selecter];
            }
            );
        centerStopButton.onClick.AddListener(
            () =>
            {
                centerStop = true;
                center.text = section[selecter];
                
            }
            );
         rightStopButton.onClick.AddListener(
            () =>
            {
                rightStop = true;
                right.text = name[selecter];
            }
            );
        startButton.onClick.AddListener(
            () =>
            {
                reset();
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

    public void reset()
    {
        selecter = r.Next(part.Count);
    }
}
