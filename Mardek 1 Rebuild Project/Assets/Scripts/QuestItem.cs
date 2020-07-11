using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
	private int qid = 0;
	private string name;
	private Sprite img;
	private string desc;
	
	public QuestItem(int idIn, string nameIn, Sprite imgIn, string descIn)
	{
		qid = idIn;
		name = nameIn;
		img = imgIn;
		desc = descIn;
	}
	
	public int getID()
	{
		return qid;
	}
}