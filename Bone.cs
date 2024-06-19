using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Bone
    {
        public int Number { get; set; }
        public int First { get; set; }
        public int Second { get; set; }
        public int Summa { get; set; }

        public Bone(int number)
        {
            Random random = new Random();
            Number = number;
            First = random.Next(1, 7);
            Second = random.Next(1, 7);
            Summa = First + Second;
        }
    }
}
