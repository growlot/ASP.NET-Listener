// <copyright file="OpenTransactionRequestWrapper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using Newtonsoft.Json;

    public class OpenTransactionRequestWrapper<T>
            where T : BaseListenerRequestMessage
    {
        public OpenTransactionRequestWrapper(T data)
        {
            this.EntityCategory = data.EntityCategory;
            this.OperationKey = data.OperationKey;
            this.Body = JsonConvert.SerializeObject(data);
        }

        public string EntityCategory { get; set; }

        public string OperationKey { get; set; }

        public string Body { get; set; }
    }
}
