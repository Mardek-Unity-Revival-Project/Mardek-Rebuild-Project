using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
	private int qid = 0;
	private String name;
	private Sprite img;
	private String desc;
	
	public QuestItem(int idIn, String nameIn, Sprite imgIn, String descIn)
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