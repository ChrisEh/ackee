using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Shared.Services
{
    public class OrderOption
    {
        public string Value { get; set; }
        public string Text { get; set; }
        
        public OrderOption(string value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
