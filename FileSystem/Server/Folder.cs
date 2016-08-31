using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   public class Folder : Item
    {
        public Folder(string name) : base(name) { list = new List<Item>(); } 
        
        public List<Item> list { get; set; }

        public bool IsChild(Folder item)
        {
            var current = item;
            while (current.Parent != null)
            {
                if (current.Parent != this)
                {
                    current = current.Parent;
                }

                else return true;
            }
            return false;
        }

        public override Item Copy()
        {
            var copy = (Folder)base.Copy();
            copy.list = new List<Item>(this.list);
            return copy;
        }


    }
}
