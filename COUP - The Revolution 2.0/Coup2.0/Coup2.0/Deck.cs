using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{

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

            for (int i = 0; i < amount; i++)
            {
                Random randomNumberGenerator = new Random();
                int cardToBeRemoved = randomNumberGenerator.Next(DeckContent.Count);
                returnList.Add(DeckContent[cardToBeRemoved]);
                DeckContent.RemoveAt(cardToBeRemoved);
                //We return the removed card because this method will be called by the player to request a card from the deck.
            }
            return returnList;
        }

        private void addCardsToDeck(int maximumAmountOfEachCard, List<Card> possibleCards)
        {
            int counter = 0;
            int currentTypeOfCard = 0;

            //Switch to the next kind of card once we hit the limit for the previous kind of card.
            for (int i = 0; i < DeckSize; i++)
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

}
