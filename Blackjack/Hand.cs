using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Hand
    {
        private List<Card> cards;
        public Hand()
        {
            cards = new List<Card>();
        }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public int GetValue()
        {
            int value = cards.Sum(c => (int)c.Rank);
            int aceCount = cards.Count(c => c.Rank == Rank.Ace);
            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }
            return value;
        }
        public override string ToString()
        {
            return string.Join(", ", cards);
        }
    }
}
