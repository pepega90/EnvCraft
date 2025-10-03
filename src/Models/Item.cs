using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvCraft.src.Models
{
    /// <summary>
    /// Item, menyimpan 2 field yaitu key dan value, merepresentasikan key value pair di ENV
    /// </summary>
    public class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
