using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   public class Item
    {
        public Item(string name)
        {
            Name = name;
         
        }

        public string Name { get; set; }
        
        public Folder Parent { get; set; }

        public virtual Item Copy()
        {
            Item copy = (Item)this.MemberwiseClone();
            copy.Name = String.Copy(Name);
            copy.Parent = null;
            return copy;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
