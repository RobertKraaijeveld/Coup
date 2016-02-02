using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand_fillHand : MonoBehaviour {

	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{
		GameObject Card1;
		GameObject Card2;
		//how about we give every card-class its own gameobject?
	}

	public List<Card> HandContent = new List<Card>();
	private readonly int HandSize = 2;
	
	public void fillHand(Deck_base deck)
	{
		for (int i = 0; i < HandSize; i++)
		{
			//HandContent.Add(deck.DrawCard());
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
