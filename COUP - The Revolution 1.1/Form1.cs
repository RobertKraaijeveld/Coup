using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
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
         *TO-DO: - CARD ABILITIES AFMAKEN (CONTEXT)
         *       - CARD PROPERTIES MAKEN
         *       - COMPULSORY COUP AND ERROR REPORTING MAKEN
         *       - PLAYER VISIBILITY MAKEN
         *       - BLUFF AND BELIEF MAKEN
         *       - TURN-SYSTEEM MAKEN
         *       - ONLINE OF MEERSPELERVERSIE MAKEN
         */

        /* --------------------
        * CLASSES FOR DECK AND HANDS
        * --------------------
        * 
        * 
        */

        public class Deck
        {
            public List<Card> DeckContent = new List<Card>();
            private int DeckSize = 10;
            //Een static randomnumbergen zorgt ervoor dat er geen herhalingen voorkomen
            static Random RandomNumberGen = new Random();

            public Deck()
            {
                for(int i = 0; i < DeckSize; i++)
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

            private void Fill()
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
                        DeckContent.Add(new Ambassador());
                        break;
                    case 5:
                        DeckContent.Add(new Assassin());
                        break;
                }
            }
        }

        /*
         * --------------------------------
         * CLASSES FOR CARDS AND CARDACTIONS
         * ---------------------------------
         * 
         */

        public abstract class Card
        {
            public abstract void Action();
        }

        public class Context
        {
            public Player player;
            public Player targetPlayer;
            public ChipStack chipStack;
            public Deck deck;
        }


        /*
        public abstract class PassiveCard : Card
        {
            public abstract void Action();
        }

        public abstract class DeckInteractionCard : Card
        {
            public abstract void Action(Player player, Deck deck);
        }

        public abstract class MoneyCard : Card
        {
            public abstract void Action(Player player, ChipStack chipStack);
        }

        public abstract class TargetCard : Card
        {
            public abstract void Action(Player player, Player targetPlayer);
        }

        */

        class Duke : Card
        {
            public override void Action(Context c)
            {
                for (int i = 0; i < 3; i++)
                {
                    c.player.TakeChip(c.chipStack);
                }
            }

            public override string ToString()
            {
                return "Duke";
            }
        }


        class Captain : Card
        {
            //this card gives passive protection: how do we do this
            public override void Action(Context c)
            {
               if (c.targetPlayer.PlayerChips.Count >= 2)
               {
                    c.targetPlayer.GiveChip(2, c.player);
               }
            }

            public override string ToString()
            {
                return "Captain";
            }
        }


        class Contessa : Card
        {
            //this card gives passive protection: how do we do this:
            //give the receiving player the option to bluff, show that he really has a contessa, or deny the other having an assasin
            public override void Action(Context c)
            {
                throw new NotImplementedException();
            }

            public override string ToString()
            {
                return "Contessa";
            }
        }

        class Ambassador : Card
        {
            //this card gives passive protection: how do we do this
            public override void Action(Context c)
            {
                //open dialog: which 2 cards would you like to keep?
                //(now, this action just doubles the players hand)
                for (int i = 0; i < 2; i++)
                {
                    c.player.PlayerHand.HandContent.Add(c.deck.DrawCard());
                }
            }
            public override string ToString()
            {
                return "Ambassador";
            }
        }


        class Assassin : Card
        {
            public override void Action(Context c)
            {
                if (c.player.PlayerChips.Count >= 3 && c.targetPlayer.FoldedAllCards == false)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        c.player.PlayerChips.RemoveAt(c.player.PlayerChips.Count - 1);
                    }
                    c.targetPlayer.FoldCard();
                }
            }
             
            public override string ToString()
            {
                return "Assassin";
            }
        }

        /*
        * --------------------------------
        * CLASSES FOR CARDS AND CARDACTIONS
        * ---------------------------------
        * 
        */

        public class Hand
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
         * END CLASSES FOR DECK AND HANDS
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

        public class Chip {}

        public class ChipStack
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

        public class Player
        {
            public List<Chip> PlayerChips = new List<Chip>();
            public Hand PlayerHand = new Hand();
            public bool FoldedAllCards = false;

            string PlayerName = "defaultPlayer";

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
                else
                {
                    //display error: You do not have enough money, or the player has already folded
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
                // a (bluff)cardaction against another player, a deny, a block or a bluffblock
                var c = new Context() { }; 
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
           List<Player> Players = new List<Player>();
           Player winner;

           public Game(int amountOfPlayers, Deck deck)
           {
               for (int i = 0; i < amountOfPlayers; i++)
               {
                   Players.Add(new Player(deck));
               }
           }

           public Player Turn()
           {
               //release controls for this player: add gui first
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
