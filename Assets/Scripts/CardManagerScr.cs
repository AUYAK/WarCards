using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense;

    public Card(string name, string logoPath, int atack, int defense)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = atack;
        Defense = defense;
    }

}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    private void Awake()
    {
        CardManager.AllCards.Add(new Card("Boozer", "Sprites/Cards/Boozer", 3, 4));
        CardManager.AllCards.Add(new Card("Cadet", "Sprites/Cards/Cadet", 228, 332));
        CardManager.AllCards.Add(new Card("Fidel-Castro", "Sprites/Cards/Fidel-Castro", 21, 21));
        CardManager.AllCards.Add(new Card("Junkie", "Sprites/Cards/Junkie", 7, 1));
        CardManager.AllCards.Add(new Card("ShturmBannFurer", "Sprites/Cards/ShturmBannFurer", 13, 13));
        CardManager.AllCards.Add(new Card("Vitas", "Sprites/Cards/Vitas", 23, 48));

    }
}
