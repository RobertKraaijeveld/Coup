using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
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
}
