using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlackJack.BlackJack;

namespace Blackjack
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack = 10,
        Queen = 10,
        King = 10,
        Ace = 11
    }
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
