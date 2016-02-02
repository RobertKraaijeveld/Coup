using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
    public interface Card
    {
        void Action(Player player, Player targetPlayer, ChipStack chipStack);
        string getCardName();
    }

}
