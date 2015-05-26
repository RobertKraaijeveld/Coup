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
    }

    /*
     * ---------------
     * END UTILITIES
     * --------------
     */

    /*
     * ---------------
     * START PROGRAM
     * --------------
     */

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
            //Een static randomnumbergen zorgt ervoor dat er geen herhalingen voorkomen
            static Random RandomNumberGen = new Random();


            public Deck()
            {
                Fill();
            }

            public Card DrawCard()
            {
                Card cardToBeRemoved = DeckContent[RandomNumberGen.Next(DeckContent.Count)];
                DeckContent.RemoveAt(DeckContent.Count - 1);
                return cardToBeRemoved;
            }

            public void Fill()
            {
                int TotalCards = 0;
                if (TotalCards < DeckSize)
                {
                    for (int DukeCount = 0; DukeCount < 3; DukeCount++)
                    {
                        DeckContent.Add(new Duke());
                    }

                    for (int CaptainCount = 0; CaptainCount < 3; CaptainCount++)
                    {
                        DeckContent.Add(new Captain());
                    }

                    for (int AssassinCount = 0; AssassinCount < 3; AssassinCount++)
                    {
                        DeckContent.Add(new Assassin());
                    }

                    for (int AmbassadorCount = 0; AmbassadorCount < 3; AmbassadorCount++)
                    {
                        DeckContent.Add(new Ambassador());
                    }

                    for (int InquisitorCount = 0; InquisitorCount < 3; InquisitorCount++)
                    {
                        DeckContent.Add(new Inquisitor());
                    }
                }
                else
                {
                    utilities.Shuffle(DeckContent);
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
            void Action(Player player, Player targetPlayer, ChipStack chipStack);
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
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
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
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
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
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {
            }

            //this card gives passive protection: how do we do this: Deny assasins the right to kill contessas in their method
            public override string ToString()
            {
                return "Contessa";
            }
        }


        class Ambassador : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {
            }

            public override string ToString()
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
                }
            }

            public override string ToString()
            {
                return "Assassin";
            }
        }

        class Inquisitor : Card
        {
            public void Action(Player player, Player targetPlayer, ChipStack chipStack)
            {

            }

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

        public class Hand
        {
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

        public class Chip { }

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
            //Wordt later in de GUI bepaald
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
            Game myGame = new Game(1);
            label1.Text += "You are holding a ";
            label1.Text += myGame.playerList[myGame.playerList.Count - 1].PlayerHand.HandContent[0].ToString();
            label1.Text += " and a ";
            label1.Text += myGame.playerList[myGame.playerList.Count - 1].PlayerHand.HandContent[1].ToString();
        }

    }

}