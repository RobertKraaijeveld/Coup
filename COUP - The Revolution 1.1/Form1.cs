using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace COUP___The_Revolution_1._1
{
    public partial class Form1 : Form
    {
        /*
         *TO-DO: - CARD ABILITIES AFMAKEN
         *       - CARD PROPERTIES MAKEN
         *       - COMPULSORY COUP AND ERROR REPORTING MAKEN
         *       - PLAYER VISIBILITY MAKEN
         *       - BLUFF AND BELIEF MAKEN: CLASHES
         *       - TURN-SYSTEEM MAKEN
         *       - ONLINE OF MEERSPELERVERSIE MAKEN
         */

        /* --------------------
        * DECK
        * --------------------
        * 
        * 
        */

        public class Deck
        {
            public List<Card> DeckContent = new List<Card>();
            private int DeckSize = 15;
            //Deze int bepaalt hoeveel van elk type card er in het deck zitten
            private readonly int amountOfEachCard = 3;
            //Een static randomnumbergen zorgt ervoor dat er geen herhalingen voorkomen
            static Random RandomNumberGen = new Random();



            public Deck()
            {
                for (int i = 0; i < DeckSize; i++)
                {
                    Fill();
                }
            }

            public Card DrawCard()
            {
                Card cardToBeRemoved = DeckContent[RandomNumberGen.Next(DeckContent.Count)];
                DeckContent.RemoveAt(DeckContent.Count - 1);
                return cardToBeRemoved;
            }

            public void Fill()
            {
                int randomNumber = RandomNumberGen.Next(8);
                switch (randomNumber)
                {
                    case 1:
                        DeckContent.Add(new Duke());
                        break;
                    case 2:
                        DeckContent.Add(new Captain());
                        break;
                    case 3:
                        DeckContent.Add(new Contessa());
                        break;
                    case 4:
                        DeckContent.Add(new Contessa());
                        break;
                    case 5:
                        DeckContent.Add(new Ambassador());
                        break;
                    case 6:
                        DeckContent.Add(new Assassin());
                        break;
                    case 7:
                        DeckContent.Add(new Inquisitor());
                        break;
                }
            }


            private void RecursiveFill()
            {
                if (DeckContent.Count != null && CheckDeck() == false)
                {
                    //loop again
                    Fill();
                }
                //else, stop looping
            }

            private bool CheckDeck()
            {
                //We loopen door alle kaarttypes in het deck.
                //Als het totale aantal kaarten 15 is en er van iedere kaart 3
                //in het deck zitten, returnen we true.

                int totalCards = 0;
                int amountOfDukes = 0;
                foreach(Duke d in DeckContent)
                {
                    amountOfDukes++;
                    totalCards++;
                }

                int amountOfCaptains = 0;
                foreach (Captain ca in DeckContent)
                {
                    amountOfCaptains++;
                    totalCards++;
                }

                int amountOfContessas = 0;
                foreach (Contessa co in DeckContent)
                {
                    amountOfContessas++;
                    totalCards++;
                }

                int amountOfAmbassadors = 0;
                foreach (Ambassador am in DeckContent)
                {
                    amountOfAmbassadors++;
                    totalCards++;
                }

                int amountOfAssasins = 0;
                foreach (Assassin ass in DeckContent)
                {
                    amountOfAssasins++;
                    totalCards++;
                }

                if (amountOfDukes == 3 &&
                amountOfCaptains == 3 &&
                amountOfContessas == 3 &&
                amountOfAmbassadors == 3 &&
                amountOfAssasins == 3 &&
                totalCards == amountOfEachCard)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
        * ---------------------
        * END DECK
        * ---------------------
        * 
        */
        

        /*
        * ---------------------
        * CARD
        * ---------------------
        * 
        */
        
         
        public interface Card
        {
            void Action();
            void Action(Player player, ChipStack chipStack);
            void Action(Player player, Player targetPlayer);
        }

        /*
        * ---------------------
        * END CARD
        * ---------------------
        * 
        */
        

        /* --------------------
        * CLASSES FOR CARDTYPES
        * --------------------
        */

        class Duke : Card
        {
            public void Action(Player player, ChipStack chipStack)
            {

                if (player.PlayerHand.HandContent.Contains(this))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player.TakeChip(chipStack);
                    }
                }
                else
                {
                    //display error: you do not have that card!
                }
            }

            public override string ToString()
            {
                return "Duke";
            }
        }


        class Captain : Card
        {
            public void Action(Player player, Player targetPlayer)
            {
                if (player.PlayerHand.HandContent.Contains(this))
                {
                    if (targetPlayer.PlayerChips.Count >= 1)
                    {
                        targetPlayer.GiveChip(2, targetPlayer);
                    }
                }
                else
                {
                    //display error: you do not have that card!
                }
            }

            public override string ToString()
            {
                return "Captain";
            }
        }


        class Contessa : Card
        {
            //this card gives passive protection: how do we do this: Deny assasins the right to kill contessas in their method
            public override string ToString()
            {
                return "Contessa";
            }
        }


        class Ambassador : Card
        {
            public override string ToString()
            {
                return "Ambassador";
            }
        }


        class Assassin : Card
        {
            public void Action(Player player, Player targetPlayer)
            {
                if (player.PlayerHand.HandContent.Contains(this) && player.PlayerChips.Count >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player.PlayerChips.RemoveAt(player.PlayerChips.Count - 1);
                    }
                    targetPlayer.FoldCard();
                }
            }
             
            public override string ToString()
            {
                return "Assassin";
            }
        }


        class Inquisitor : Card
        {
            public override string ToString()
            {
                return "Inquisitor";
            }
        }


        /* ------------------------
        * END CLASSES FOR CARDTYPES
        * -------------------------
        * 
        */

        /*
        * ---------------------
        * HAND
        * ---------------------
        * 
        */

        class Hand
        {
            //Gotta display errormessages
            public List<Card> HandContent = new List<Card>();
            private readonly int HandSize = 2;

            public void fillHand(Deck deck)
            {
                for (int i = 0; i < HandSize; i++)
                {
                    HandContent.Add(deck.DrawCard());
                }
            }
        }

        /*
         * ---------------------
         * END HAND
         * ---------------------
         * 
         */


        /*
         * 
         * -----------------------
         * CLASSES FOR CHPS
         * -----------------------
         * 
         * 
         */

        class Chip {}

        class ChipStack
        {
            public List<Chip> ChipStackContent = new List<Chip>();
            private readonly int StackSize = 65;
            static Random RandomNumberGen = new Random();

            public ChipStack()
            {
                for (int i = 0; i < StackSize; i++)
                {
                    ChipStackContent.Add(new Chip());
                }
            }

            public Chip TakeChip()
            {
                Chip chipToBeRemoved = ChipStackContent[RandomNumberGen.Next(ChipStackContent.Count)];
                ChipStackContent.RemoveAt(ChipStackContent.Count - 1);
                return chipToBeRemoved;
            }
        }


        /*
         * ---------------------
         * END CLASSES FOR CHIPS
         * ---------------------
         * 
         */


        /*
         * 
         * -----------------------
         * CLASSES FOR PLAYER
         * -----------------------
         * TO DO: ADD BLUFF AND BELIEVES
         * 
         */

        class Player
        {
            public List<Chip> PlayerChips = new List<Chip>();
            public Hand PlayerHand = new Hand();
            public bool FoldedAllCards = false;

            public Player(Deck deck)
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
                PlayerChips.RemoveAt(PlayerChips.Count - 2);
            }

            public void Coup(Player targetPlayer)
            {
                if (this.FoldedAllCards == false && targetPlayer.FoldedAllCards == false && this.PlayerChips.Count >= 7)
                {
                    targetPlayer.FoldCard();
                }
            }

            public void TakeChip(ChipStack chipStack)
            {
                if (this.PlayerChips.Count < 10)
                {
                    chipStack.TakeChip();
                }
                else
                {
                    //display error: you have to coup!
                }
            }

            public void TakeChipForeignAid(ChipStack chipStack)
            {
                if (this.PlayerChips.Count < 10)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        chipStack.TakeChip();
                    }
                }
                else
                {
                    //display error: you have to coup!
                }
            }

            public void GiveChip(int amount, Player receivingPlayer)
            {
                if (this.PlayerChips.Count >= amount)
                {
                    for(int i = 0; i < amount; i++)
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
                card.Action();
            }

        }

        /*
        * ---------------------
        * END CLASSES FOR PLAYER
        * ---------------------
        * 
        */

        /*
        * ---------------------
        * CLASSES FOR GAME 
        * ---------------------
        * 
        */

        class Game
        {


           public Game(int amountOfPlayers)
           {

           }
        }

        /*
        * ---------------------
        * END CLASSES FOR GAME
        * ---------------------
        * 
        */

        /*
         * -----------
         * STATIC MAIN
         * -----------
         */

        //This is pretty much my Static Main.
        public Form1()
        { 
            InitializeComponent();
            Deck myDeck = new Deck();
            Player myPlayer = new Player(myDeck);
            //todo: display labelshit in a method instead of here
        }
 
    }
 
}
