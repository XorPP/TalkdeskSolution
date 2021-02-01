using System;
using System.Collections.Generic;
using System.Text;

namespace Talkdesk.Model
{
    public class NumberData
    {
        private string _prefix;
        private string _businessSector;
        private int _numberOfOccur;
        //private string _number;

        public int NumberOfOccur { get => _numberOfOccur; set => _numberOfOccur = value; }
        public string BusinessSector { get => _businessSector; set => _businessSector = value; }
        public string Prefix { get => _prefix; set => _prefix = value; }
    }
}
