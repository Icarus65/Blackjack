using Blackjack;
using System;

namespace BlackJack
{
    internal class BlackJackGame
    {
        private readonly Player _player = new Player();

        public enum GameResult
        {
            PlayerWins,
            DealerWins,
            Push
        }

        public void StartGame()
        {
            Console.WriteLine("What is your name?");
            _player.PlayerName = Console.ReadLine();

            _player.Currency = ReadDouble("How much money are you spending today?");

            while (_player.Currency > 0)
            {
                PlayRound();

                Console.WriteLine($"Balance: {_player.Currency}");

                Console.WriteLine("Play another round? (y/n)");
                if (Console.ReadLine()?.ToLower() != "y")
                    break;
            }

            Console.WriteLine("Game over!");
        }

        private void PlayRound()
        {
            double betAmount = ReadBet();
            _player.Currency -= betAmount;

            Deck deck = new Deck();
            deck.Shuffle();

            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();

            DealInitialCards(deck, playerHand, dealerHand);

            if (PlayerTurn(deck, playerHand))
            {
                Console.WriteLine("Player busts! Dealer wins.");
                return;
            }

            DealerTurn(deck, dealerHand);

            GameResult result = DetermineWinner(playerHand, dealerHand);
            ResolveRound(result, betAmount);
        }

        private void DealInitialCards(Deck deck, Hand playerHand, Hand dealerHand)
        {
            playerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());

            Console.WriteLine($"Player: {playerHand} ({playerHand.GetValue()})");
            Console.WriteLine($"Dealer: {dealerHand} ({dealerHand.GetValue()})");
        }

        private bool PlayerTurn(Deck deck, Hand playerHand)
        {
            while (true)
            {
                Console.WriteLine("Hit or Stand? (h/s)");
                string input = Console.ReadLine()?.ToLower();

                if (input == "h")
                {
                    playerHand.AddCard(deck.DealCard());
                    Console.WriteLine($"Player: {playerHand} ({playerHand.GetValue()})");

                    if (playerHand.GetValue() > 21)
                        return true;
                }
                else if (input == "s")
                {
                    return false;
                }
            }
        }

        private void DealerTurn(Deck deck, Hand dealerHand)
        {
            while (dealerHand.GetValue() < 17)
            {
                dealerHand.AddCard(deck.DealCard());
                Console.WriteLine($"Dealer: {dealerHand} ({dealerHand.GetValue()})");
            }
        }

        private GameResult DetermineWinner(Hand playerHand, Hand dealerHand)
        {
            int playerValue = playerHand.GetValue();
            int dealerValue = dealerHand.GetValue();

            if (dealerValue > 21 || playerValue > dealerValue)
                return GameResult.PlayerWins;

            if (playerValue < dealerValue)
                return GameResult.DealerWins;

            return GameResult.Push;
        }

        private void ResolveRound(GameResult result, double betAmount)
        {
            switch (result)
            {
                case GameResult.PlayerWins:
                    Console.WriteLine("Player wins!");
                    _player.Currency += betAmount * 2;
                    break;

                case GameResult.DealerWins:
                    Console.WriteLine("Dealer wins!");
                    break;

                case GameResult.Push:
                    Console.WriteLine("Push! Nothing happened...");
                    _player.Currency += betAmount;
                    break;
            }
        }
        private double ReadBet()
        {
            while (true)
            {
                double bet = ReadDouble("Place your bet:");

                if (bet <= 0)
                    Console.WriteLine("Bet must be positive.");
                else if (bet > _player.Currency)
                    Console.WriteLine("You can't bet more than your balance.");
                else
                    return bet;
            }
        }
        private double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Invalid number.");
            }
        }
    }
}
