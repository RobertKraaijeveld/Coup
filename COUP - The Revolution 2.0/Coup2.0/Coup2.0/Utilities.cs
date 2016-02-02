using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
    /*
    * ---------------------------
    * MISCELLANEOUS UTILITIES
    * ---------------------------
    */

    public static class utilities
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void WriteIntroMessage()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("WELCOME, CHANCELLOR, TO COUP: THE REVOLUTION!");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("Welcome to Coup, a game of wits, deception, deceit and cunning!");
            Console.WriteLine("Coup is a card game played by up to 5 people. Each Coup game is made up of turns.");
            Console.WriteLine("Coup is played until only one player who still has cards left remains.");

            Console.WriteLine("");

            Console.WriteLine("The setup is as follows:");
            Console.WriteLine("--------------CARDS------------------");
            Console.WriteLine("Each player gets dealt two cards. Each card has special abilities, which we'll look at later.");
            Console.WriteLine("In the Coup universe, a card is known as 'influence'. Being forced to fold a card is known as 'losing influence'.");

            Console.WriteLine("--------------CHIPS------------------");
            Console.WriteLine("Each player also gets dealt 2 chips at the start of a game of Coup.");
            Console.WriteLine("Chips are the going currency for ");

            Console.WriteLine("");

            Console.WriteLine("THE CAPTAIN: An old veteran of the imperial fleet, this commander has turned his raiding skills to different use..");
            Console.WriteLine("The captain allows you to steal 2 chips (or less, if the player has less than 3 chips) from another player. This action is free.");
            Console.WriteLine("The captain can be blocked from stealing by another captain, or by an ambassador.");

            Console.WriteLine("THE ASSASSIN: As her name implies, the assassin is a lady who uses her talent for stealth and subterfuge to silently dispose of whoever she desires..");
        }

    }

    /*
     * ---------------
     * END UTILITIES
     * --------------
     */

}
