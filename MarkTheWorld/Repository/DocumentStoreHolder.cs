using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store
        {
            get { return store.Value; }
        }

        private static IDocumentStore CreateStore()
        {
            // init
            var tmpStore = new DocumentStore
            {
                ConnectionStringName = "ravenDB"
            }
            .Initialize();

            // store indexes
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), tmpStore);

            // store facets
            using (var tmpSession = tmpStore.OpenSession())
            {
                var facetSetups =
                    Assembly.GetExecutingAssembly().DefinedTypes
                    .Where(type => typeof(FacetSetup).IsAssignableFrom(type))
                    .ToList();
                foreach (var facetSetupInstance in facetSetups.Select(Activator.CreateInstance))
                {
                    tmpSession.Store(facetSetupInstance);
                }
                tmpSession.SaveChanges();
            }

            tmpStore.Conventions.IdentityPartsSeparator = "-";

            return tmpStore;
        }
    }
}
