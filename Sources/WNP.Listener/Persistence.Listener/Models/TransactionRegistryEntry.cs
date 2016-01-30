using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener.Models
{
    public class TransactionRegistryEntry
    {
        /// <summary>
        /// Gets or sets the transaction registry entity.
        /// </summary>
        /// <value>The transaction registry entity.</value>
        public TransactionRegistryEntity TransactionRegistryEntity { get; set; }

        /// <summary>
        /// Gets or sets the entity category operation.
        /// </summary>
        /// <value>The entity category operation.</value>
        public EntityCategoryOperationEntity EntityCategoryOperation { get; set; }
    }
}
