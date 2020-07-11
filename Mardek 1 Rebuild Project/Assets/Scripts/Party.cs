//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Party : MonoBehaviour
//{
//	private Fighter[] characterList = new Fighter[4];
//	//This will store the list of characters for this chapter,
//	//in this case the two versions of Mardek and Deugan.
	
//	private int listTotal = 0;
//	//The number of characters in the list, for the array-manip methods to reference.
	
//	private Fighter[] currentParty = new Fighter[4];
//	//This will store the player's current party.
	
//	private int partyTotal;
//	//Stores the number of characters in the party, for the array-manip methods to reference.
	
//	private QuestItem[] macguffinList = new QuestItem[1];
//	//This will store the party's quest items.
	
//	private int numQI = 0;
//	//The number of quest items the party currently has, for array-manip methods to reference.
	
//	private int gold = 0;
//	//The party's current gold.
	
	
//	public void addChar(Fighter chara)
//	{
//		characterList[chara.id] = chara;
//		listTotal++;
//	}
//	//Adds a new character to the first open spot in the CharacterList and increments listTotal accordingly.
	
//	public void swapChar(Entity swapOut, Entity swapIn)
//	{
//		for (int i=0; i<listTotal; i++)
//		{
//			if (characterList[i].getThing().equals(swapOut.getThing()))
//				characterList[i] = swapIn;
//		}
//	}
//	//Swaps the first specified character (if acquired) for the second specified character. Niche use-cases.
	
//	public void removeChar(Entity chara)
//	{
//		for (int i=0; i<listTotal; i++)
//		{
//			if (characterList[i].getThing().equals(chara.getThing()))
//			{
//				characterList[i] = characterList[listTotal-1];
//				listTotal--;
//			}
//		}
//	}
//	//Removes a character from the list and decrements the listTotal accordingly.
	
//	public Entity getChar(int r)
//	{
//		if (r < listTotal)
//			return characterList[r];
//	}
//	//Returns the entitiy at the specified index of the list if it's possible for that index to be filled.
	
//	public void partyAdd(Entity chara, int index)
//	{
//		if (index < 4)
//		{
//			if (!currentParty[index].isNecessary())
//				currentParty[index] = chara;
//		}
//	}
//	//Adds a character to the party at a specified index
	
//	public void partyRemove(Entity chara)
//	{
//		for (int i=0; i<4; i++)
//		{
//			if (currentParty[i].getThing().equals(chara.getThing()))
//				currentParty[i] = null;
//		}
//	}
//	//Annuls a party slot if that slot is occupied by the input character.
	
//	public void macguffinAdd(QuestItem toAdd)
//	{
//		macGuffinList[toAdd.getID] = toAdd;
//	}
//	//Inserts a new Quest Item into the list.
	
//	public void macguffinRemove(QuestItem toRemove)
//	{
//		macGuffinList[toRemove.getID] = null;
//	}
//	//Annuls an item in the QuestItem array.
	
//	public void addGold(int g)
//	{
//		gold += g;
//	}
//	//Adds gold to the player's current total.
	
//	public void removeGold(int g)
//	{
//		gold -= g;
//	}
//	//Subtracts gold from the player's current total.
//}