using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{



        /* --------------------
        * CLASSES FOR CARDTYPES (refactoring)
        * --------------------
        */

        /*
         * Issues with these:
         *  X TakeChip should not be a player method, but a chipstack method and should take an amounts-param.
         *  - Same goes for GiveChip.
         *  - We should add a player method: AddChipToPurse(), which takes a chip from the stack and add it to the players chips.
         *  X ToString() -> getCardType()
         *  X PlayerHand.Contains and everything is unneccessary; the UI will take care of that. The missing card will never show up.
         *  - ..
         *  
         * 
         */

        class Duke : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {

            }

            public string getCardName()
            {
                return "Duke";
            }
        }


        class Captain : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {

                for (int i = 0; i < targetPlayer.PlayerChips.Count; i++)
                {
                    if (i > 1 && i < 3)
                    {
                        targetPlayer.GiveChip(i, player);
                    }
                    else if (i > 3)
                    {
                        targetPlayer.GiveChip(3, player);
                    }
                    else if (i < 1)
                    {
                        //error: no chips to steal
                    }
                }
            }

            public string getCardName()
            {
                return "Captain";
            }
        }


        class Contessa : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {
            }

            //this card gives passive protection: how do we do this: Deny assasins the right to kill contessas in their method
            public string getCardName()
            {
                return "Contessa";
            }
        }


        class Ambassador : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {
            }

            public string getCardName()
            {
                return "Ambassador";
            }
        }


        class Assassin : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {
                if (player.PlayerHand.HandContent.Contains(this) && player.PlayerChips.Count >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player.PlayerChips.RemoveAt(player.PlayerChips.Count - 1);
                    }
                    targetPlayer.FoldCard();
                    //keuze geven in GUI
                }
            }

            public string getCardName()
            {
                return "Assassin";
            }
        }

        class Inquisitor : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {

            }

            public string getCardName()
            {
                return "Inquisitor";
            }
        }

}
