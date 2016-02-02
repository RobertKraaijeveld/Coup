using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
        /*
         * 
         * -----------------------
         * CLASSES FOR PLAYER
         * -----------------------
         * TO DO: ADD BLUFF AND BELIEVES
         * 
         * 
         * Issues with these:
         *  - TakeChip should not be a player method, but a chipstack method and should take an amounts-param.
         *  - Same goes for GiveChip.
         *  - .. 
         * 
         */

    public class Player
    {
        public String Name;

        public Game gameSession;
        public List<Chip> PlayerChips = new List<Chip>();
        public Hand PlayerHand = new Hand();
        public bool FoldedAllCards = false;

        public Player(Deck deck, String playerName)
        {
            PlayerHand = new Hand();
            PlayerHand.fillHand(deck);

            //initial 2 chips
            for (int i = 0; i < 2; i++)
            {
                PlayerChips.Add(new Chip());
            }
        }

        public void FoldCard()
        {
            //make player be able to chose which card to fold
            PlayerHand.HandContent.RemoveAt(PlayerHand.HandContent.Count - 2);
        }

        public void Coup(Player targetPlayer)
        {
            if (this.FoldedAllCards == false && targetPlayer.FoldedAllCards == false && this.PlayerChips.Count >= 7)
            {
                targetPlayer.FoldCard();
            }
        }

        public void GiveChip(int amount, Player receivingPlayer)
        {
            if (this.PlayerChips.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    this.PlayerChips.RemoveAt(this.PlayerChips.Count - 1);
                    receivingPlayer.PlayerChips.Add(new Chip());
                }
            }
            else
            {
                //display error: You have no chips to give!
            }
        }

        public void CardAction(Card card)
        {
            //als er een targetplayer is verandert deze methode signature
            //de speler kan dit aangeven dmv de gui: De knop met "steal" heeft wel een targetplayer,
            // "Grab 3 coins" natuurlijk niet
            card.Action(this, this, gameSession.gameChipStack);
        }

    }
}
