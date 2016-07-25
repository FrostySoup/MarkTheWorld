using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DeleteLater;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace Repository.Delete
{
    public class ItemRepository
    {
        public async Task<List<Item>> GetAllItems()
        {
            List<Item> items = new List<Item>();
            IDocumentQuery<Item> query = DocumentDBRepository<Item>.getDoc().CreateDocumentQuery<Item>(
                UriFactory.CreateDocumentCollectionUri(DocumentDBRepository<Item>.DbId(), DocumentDBRepository<Item>.CollectId(0)))
               .Where(x => x.Completed)
               .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                items.AddRange(await query.ExecuteNextAsync<Item>());
            }

            return items;
        }

        public async Task AddItem(Item item)
        {
            await DocumentDBRepository<Item>.getDoc().CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DocumentDBRepository<Item>.DbId(), DocumentDBRepository<Item>.CollectId(0)), item);
        }
    }
}
