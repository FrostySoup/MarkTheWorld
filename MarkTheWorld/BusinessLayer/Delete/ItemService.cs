using Data.DeleteLater;
using Repository.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Delete
{
    public class ItemService
    {

        private ItemRepository itemRep = new ItemRepository();

        public async Task<List<Item>> getItems()
        {
            return await itemRep.GetAllItems();
        }

        public async Task addItem(Item item)
        {
            await itemRep.AddItem(item);
        }
    }
}
