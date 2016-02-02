using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
    /*
     * ---------------------
     * CLASSES FOR GAME 
     * ---------------------
     * 
     */

    public class Game
    {
        public List<Player> playerList = new List<Player>();
        public Deck gameDeck;
        public ChipStack gameChipStack;

        public Game(int amountOfPlayers)
        {
            gameDeck = new Deck();
            gameChipStack = new ChipStack();
            for (int i = 0; i < amountOfPlayers; i++)
            {
                playerList.Add(new Player(gameDeck, "test"));
            }
        }
    }
}
