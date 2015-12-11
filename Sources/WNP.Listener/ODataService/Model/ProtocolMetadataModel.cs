using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService.Model
{
    public class ProtocolMetadataModel
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the common type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public CommonDataType DataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is describing an array property.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        public bool IsArray { get; set; }

        /// <summary>
        /// Gets or sets the nested model data
        /// </summary>
        /// <value>The inner data.</value>
        public ProtocolMetadataModel InnerData { get; set; }
    }
}
