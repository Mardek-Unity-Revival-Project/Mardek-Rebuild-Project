using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour
{
    public bool custom;
    private const int SIZE = 0;
    public int size; // set both sizes via inspector on same value
    private int i = 0;
    private int steps = 0;
    private int max;
    private int min;
    private int step_limit;
    private int troop_count;
    private string[][] troop;
    public string[] way = new string[SIZE];
    public int size_dialogue;
    public string[] text = new string[SIZE];
    public string[] header = new string[SIZE];
    public string[] element = new string[SIZE];
    public string[] font = new string[SIZE];
    private DialogueData[] DialogueData;
    private void Start()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        string path = SceneManager.GetActiveScene().name;
        if (ApplicationData.Custom_Played.Contains(path))
        {
            custom = false;
        }
        System.IO.StreamReader file = new System.IO.StreamReader(@"c:\Mardek\Scene Setting\" + path + ".txt");
        file.ReadLine();
        file.ReadLine();
        min = int.Parse(file.ReadLine());
        file.ReadLine();
        max = int.Parse(file.ReadLine());
        file.ReadLine();
        troop_count = int.Parse(file.ReadLine());
        troop = new string[troop_count][];
        for (int j = 0; j < troop_count; j++)
        {
            troop[j] = new string[4];
            for (int k = 0; k < 4; k++)
            {
                file.ReadLine();
                troop[j][k] = Converter(file.ReadLine());
            }
        }
        step_limit = Random.Range(min, max + 1);
    }
    public virtual int GetMovement()
    {
        if (ApplicationData.lockdown)
            return 0;
        if (custom)
        {
            if (way[i] == "LEFT")
            {
                return Constants.LEFT;
            }
            else if (way[i] == "RIGHT")
            {
                return Constants.RIGHT;
            }
            else if (way[i] == "UP")
            {
                return Constants.UP;
            }
            else if (way[i] == "DOWN")
            {
                return Constants.DOWN;
            }
            else
            {
                return 0;
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return Constants.DOWN;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            return Constants.UP;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            return Constants.RIGHT;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return Constants.LEFT;
        }
        else
        {
            return 0;
        }
    }
    public virtual void Move()
    {
        if (i < size - 1)
        {
            i++;
            return;
        }
        else
        {
            if (custom == true)
            {
                CustomIsFalse();
                ApplicationData.Custom_Played.Add(SceneManager.GetActiveScene().name);
            }
        }
        steps ++;
        if (steps==step_limit)
        {
            steps = 0;
            step_limit = Random.Range(min, max + 1);
            Frame_3.InitializeBattle(troop[Random.Range(0, troop_count)]);
        }
    }
    private string Converter(string input)
    {
        int length = input.Length;
        char[] ch = new char[length - 2];
        for (int i=0; i<length-2; i++)
        {
            ch[i] = input[i + 1];
        }
        return new string(ch);
    }
    private void CustomIsFalse()
    {
        custom = false;
        if (size_dialogue != 0)
        {
            DialogueData[] DialogueData = new DialogueData[size_dialogue];
            for (int i = 0; i < size_dialogue - 1; i++)
            {
                DialogueData[i] = new DialogueData(text[i], header[i], element[i], font[i], false);
            }
            DialogueData[size_dialogue - 1] = new DialogueData(text[size_dialogue - 1], header[size_dialogue - 1], element[size_dialogue - 1], font[size_dialogue - 1], true);
            GameObject go = GameObject.Find("UI");
            DialogueMaster DialogueMaster = go.GetComponent<DialogueMaster>();
            DialogueMaster.SendData(DialogueData);
        }
    }
    public void OnParticleTrigger()
    {
        if (ApplicationData.Custom_Played.Contains(SceneManager.GetActiveScene().name))
        {
            custom = true;
        }
    }
}
