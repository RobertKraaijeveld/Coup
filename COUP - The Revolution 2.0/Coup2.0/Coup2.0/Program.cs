using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Coup2
{


    /*
     * ---------------
     * START PROGRAM
     * --------------
     */

        /*
         *TO-DO: - CARD ABILITIES AFMAKEN
         *       - CARD PROPERTIES MAKEN
         *       - COMPULSORY COUP (!) AND ERROR REPORTING MAKEN
         *       - PLAYER VISIBILITY MAKEN
         *       - BLUFF AND BELIEF MAKEN: CLASHES
         *       - TURN-SYSTEEM MAKEN
         *       - RANDOM OPPONENTS
         *
         * TO REFACTOR 
         *  - Encapsulating variables into behaviour
         *  - Making simpler, cleaner methods
         *  - using more descriptive names
         *  - Most of all: Removing duplication
         * 
         */ 

         


        /* --------------------
        * DECK (refactored)
        * --------------------
        * 
        * 
        */

        public class Deck
        {
            private List<Card> DeckContent = new List<Card>();
            private int maxAmountOfCardsPerType = 3;
            private int DeckSize = 15;

            public Deck()
            {
                this.FillDeck();
            }

            public List<Card> DrawCardFromDeck(int amount)
            {
                List<Card> returnList = new List<Card>();

                for(int i = 0; i < amount; i++)
                {
                    Random randomNumberGenerator = new Random();
                    int cardToBeRemoved = randomNumberGenerator.Next(DeckContent.Count);
                    DeckContent.RemoveAt(cardToBeRemoved);
                    //We return the removed card because this method will be called by the player to request a card from the deck.
                    returnList.Add(DeckContent[cardToBeRemoved]);
                }
                return returnList;
            }

            private void addCardsToDeck(int maximumAmountOfEachCard, List<Card> possibleCards)
            {
                int counter = 0;
                int currentTypeOfCard = 0;

                //Switch to the next card once we hit the limit for the previous card.
                for(int i = 0; i < DeckSize; i++)
                {
                    counter++;
                    if (counter == maximumAmountOfEachCard)
                    {
                        currentTypeOfCard++;
                    }
                    DeckContent.Add(possibleCards[currentTypeOfCard]);
                }
            }

            private void FillDeck()
            {  
                List<Card> possibleCards = new List<Card>() { new Duke(), new Captain(), new Assassin(), new Inquisitor() };

                addCardsToDeck(maxAmountOfCardsPerType, possibleCards);
                utilities.Shuffle(DeckContent);
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
            string getCardName();
        }

        /*
        * ---------------------
        * END CARD
        * ---------------------
        * 
        */


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
                HandContent.Concat(deck.DrawCardFromDeck(HandSize));
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

        public class Chip {}

        public class ChipStack
        {
            private List<Chip> ChipStackContent = new List<Chip>();
            private int StackSize = 65;

            public ChipStack()
            {
                for (int i = 0; i < StackSize; i++)
                {
                    ChipStackContent.Add(new Chip());
                }
            }

            public List<Chip> removeChips(int amountToTake)
            {
                List<Chip> returnList = new List<Chip>();
                Random RandomNumberGen = new Random();

                for (int i = 0; i < amountToTake; i++)
                {
                    Chip chipToBeRemoved = ChipStackContent[RandomNumberGen.Next(ChipStackContent.Count)];
                    returnList[i] = chipToBeRemoved;
                    ChipStackContent.RemoveAt(ChipStackContent.Count - 1);
                }
                return returnList;
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
    }

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

