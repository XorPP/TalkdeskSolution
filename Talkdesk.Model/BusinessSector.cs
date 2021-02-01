using System;

namespace Talkdesk.Model
{
    public class BusinessSector
    {
        private string _number;
        private string _sector;

        public string Number { get => _number; set => _number = value; }
        public string Sector { get => _sector; set => _sector = value; }
    }
}
