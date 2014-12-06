using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoverMap.dtos
{
    public class CoverInfoDTO
    {
        public string NetworkName { get; set; }
        public string Technology { get; set; }
        public int SignalStrength { get; set; }
    }
}