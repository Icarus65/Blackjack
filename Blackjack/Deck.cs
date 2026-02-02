using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Deck
    {
        public Card card;
        private List<Card> cards;
        private Random random = new Random();
        public Deck()
        {
            cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }
        public void Shuffle()
        {
            cards = cards.OrderBy(c => random.Next()).ToList();
        }
        public Card DealCard()
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck.");
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
