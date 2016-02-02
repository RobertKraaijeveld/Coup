using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coup2._0
{
    public class Chip { }

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
}
