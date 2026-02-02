using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket_Landing
{
    internal class BlackJack
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
        public class Deck
        {
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
        public enum GameResult
        {
            PlayerWins,
            DealerWins,
            Push
        }
        public GameResult DetermineWinner(Hand playerHand, Hand dealerHand)
        {
            int playerValue = playerHand.GetValue();
            int dealerValue = dealerHand.GetValue();
            if (playerValue > 21)
                return GameResult.DealerWins;
            if (dealerValue > 21)
                return GameResult.PlayerWins;
            if (playerValue > dealerValue)
                return GameResult.PlayerWins;
            if (dealerValue > playerValue)
                return GameResult.DealerWins;
            return GameResult.Push;
        }
        public void StartGame()
        {
            while (true)
            {
                PlayRound();
                Console.WriteLine("Play another round? (y/n)");
                string input = Console.ReadLine();
                if (input.ToLower() != "y")
                    break;
            }
        }
        public void PlayRound()
        {
            Deck deck = new Deck();
            deck.Shuffle();
            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();
            playerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            Console.WriteLine($"Player's Hand: {playerHand} (Value: {playerHand.GetValue()})");
            Console.WriteLine($"Dealer's Hand: {dealerHand} (Value: {dealerHand.GetValue()})");
            while (true)
            {
                Console.WriteLine("Hit or Stand? (h/s)");
                string input = Console.ReadLine();
                if (input.ToLower() == "h")
                {
                    playerHand.AddCard(deck.DealCard());
                    Console.WriteLine($"Player's Hand: {playerHand} (Value: {playerHand.GetValue()})");
                    if (playerHand.GetValue() > 21)
                    {
                        Console.WriteLine("Player busts! Dealer wins.");
                        return;
                    }
                }
                else if (input.ToLower() == "s")
                {
                    break;
                }
            }
            while (dealerHand.GetValue() < 17)
            {
                dealerHand.AddCard(deck.DealCard());
                Console.WriteLine($"Dealer's Hand: {dealerHand} (Value: {dealerHand.GetValue()})");
            }
            GameResult result = DetermineWinner(playerHand, dealerHand);
            switch (result)
            {
                case GameResult.PlayerWins:
                    Console.WriteLine("Player wins!");
                    break;
                case GameResult.DealerWins:
                    Console.WriteLine("Dealer wins!");
                    break;
                case GameResult.Push:
                    Console.WriteLine("It's a push!");
                    break;
            }
        }
        public BlackJack()
        {
        }
    }
}