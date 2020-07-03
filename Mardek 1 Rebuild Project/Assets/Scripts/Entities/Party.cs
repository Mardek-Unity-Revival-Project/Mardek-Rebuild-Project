using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
	/// <summary>
	/// An array of characters available for this chapter, in this case Mardek and Deugan.
	/// </summary>
	private Character[] characterList = new Character[2];
	
	/// <summary>
	/// The current amount of characters in the Character list.
	/// </summary>
	private int listTotal = 0;

	/// <summary>
	/// An array of the current party.
	/// </summary>
	private Character[] currentParty = new Character[4];
	
	/// <summary>
	/// The current number of characters in the party.
	/// </summary>
	private int partyTotal;

	/// <summary>
	/// A list of quest items
	/// </summary>
	private List<QuestItem> macguffinList = new List<QuestItem>();

	/// <summary>
	/// The number of quest items the party currently has, for array-manip methods to reference.
	/// </summary>
	private int numberOfQuestItems = 0;

	/// <summary>
	/// The party's current gold.
	/// </summary>
	private int gold = 0;

	/// <summary>
	/// Adds a new character to the first open spot in the CharacterList and increments listTotal accordingly.
	/// </summary>
	/// <param name="chara">The character to add</param>
	public void AddChar(Character chara)
	{
		characterList[listTotal] = chara;
		listTotal++;
	}

	/// <summary>
	/// Swaps the first specified character (if acquired) for the second specified character. Niche use-cases.
	/// </summary>
	/// <param name="swapOut">The character to be replaced</param>
	/// <param name="swapIn">The character to be placed into the party</param>
	public void SwapChar(Character swapOut, Character swapIn)
	{
		for (int i=0; i<listTotal; i++)
		{
			if (characterList[i].Equals(swapOut))
            {
                characterList[i] = swapIn;
            }
        }
	}

	/// <summary>
	/// Removes a character from the character list and decrements the listTotal accordingly.
	/// </summary>
	/// <param name="chara">The character to remove</param>
	public void RemoveChar(Character chara)
	{
		for (int i=0; i<listTotal; i++)
		{
			if (characterList[i].Equals(chara))
			{
				characterList[i] = characterList[listTotal-1];
				listTotal--;
			}
		}
	}

	/// <summary>
	/// Returns the entity at the specified index of the partylist 
	/// </summary>
	/// <param name="slot">The slot to look at. Mardek is always in slot 1, unless he's out of the party.</param>
	/// <returns></returns>
	public Character GetChar(int slot)
	{
		if (slot < listTotal)
        {
            return characterList[slot];
        }

        return null;
	}

	/// <summary>
	/// Adds a character to the party at a specified index.
	/// </summary>
	/// <param name="chara">The character to place into the party</param>
	/// <param name="index">The slot to place that character into</param>
	public void PartyAdd(Character chara, int index)
	{
		if (index < 4)
		{
			if (!currentParty[index].IsNecessary)
            {
                currentParty[index] = chara;
            }
        }
	}

	/// <summary>
	/// Annuls a party slot if that slot is occupied by the input character.
	/// </summary>
	/// <param name="chara"></param>
	public void PartyRemove(Character chara)
	{
		for (int i=0; i<4; i++)
		{
			if (currentParty[i].Equals(chara))
            {
                currentParty[i] = null;
            }
        }
	}

	/// <summary>
	/// Inserts a new Quest Item into the list.
	/// </summary>
	/// <param name="toAdd">The quest item to add</param>
	public void MacguffinAdd(QuestItem toAdd)
	{
            macguffinList.Add(toAdd);
    }
	

	/// <summary>
	/// Removes a quest item
	/// </summary>
	/// <param name="toRemove">The quest item to remove</param>
	public void MacguffinRemove(QuestItem toRemove)
	{
		macguffinList.Remove(toRemove);
	}

	/// <summary>
	/// Add or remove gold
	/// </summary>
	/// <param name="g">The amount of gold to add/remove</param>
	public void ModifyGold(int g)
	{
		gold += g;
	}
	
}