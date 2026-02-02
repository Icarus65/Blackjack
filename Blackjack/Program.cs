using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rocket_Landing.BlackJack;

namespace Rocket_Landing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BlackJack game = new BlackJack();
            game.StartGame();
        }
    }
}