using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
    using Newtonsoft.Json;

    public class OpenTransactionRequestWrapper<T>
            where T : BaseListenerRequestMessage
    {
        public string EntityCategory { get; set; }
        public string OperationKey { get; set; }

        public string Body { get; set; }
        public OpenTransactionRequestWrapper(T data)
        {
            this.EntityCategory = data.EntityCategory;
            this.OperationKey = data.OperationKey;
            this.Body = JsonConvert.SerializeObject(data);
        }
    }

}

