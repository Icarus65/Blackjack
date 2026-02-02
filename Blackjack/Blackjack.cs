using Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class BlackJack
    {
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