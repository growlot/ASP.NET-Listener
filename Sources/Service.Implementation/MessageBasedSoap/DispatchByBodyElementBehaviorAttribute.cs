//-----------------------------------------------------------------------
// <copyright file="DispatchByBodyElementBehaviorAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.MessageBasedSoap
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using System.Xml;

    /// <summary>
    /// Operation selector for contract scope.
    /// <see href="http://msdn.microsoft.com/en-us/library/aa395223(v=vs.90).aspx">Initial source</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DispatchByBodyElementBehaviorAttribute : Attribute, IContractBehavior
    {
        /// <summary>
        /// Configures any binding elements to support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract description to modify.</param>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// </summary>
        /// <param name="contractDescription">The contract description for which the extension is intended.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        /// <summary>
        /// Implement to confirm that the contract and endpoint can support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract to validate.</param>
        /// <param name="endpoint">The endpoint to validate.</param>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// First, it sets up the lookup dictionary for the operation selector by iterating over the OperationDescription
        /// elements in the service endpoint's ContractDescription. Then, each operation description is inspected for the
        /// presence of the <see cref="DispatchBodyElementAttribute"/> behavior. If such a behavior is found, a value pair
        /// created from the XML qualified name (QualifiedName property) and the operation name (Name property) is added to the dictionary.
        /// </summary>
        /// <param name="contractDescription">The contract description to be modified.</param>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="dispatchRuntime">The dispatch runtime that controls service execution.</param>
        /// <exception cref="System.ArgumentNullException">
        /// contractDescription;Can not apply behavior when contract description is not specified.
        /// or
        /// dispatchRuntime;Can not dispatch behavior when dispatch runtime is not specified.
        /// </exception>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            if (contractDescription == null)
            {
                throw new ArgumentNullException("contractDescription", "Can not apply behavior when contract description is not specified.");
            }

            if (dispatchRuntime == null)
            {
                throw new ArgumentNullException("dispatchRuntime", "Can not dispatch behavior when dispatch runtime is not specified.");
            }

            Dictionary<XmlQualifiedName, string> dispatchDictionary = new Dictionary<XmlQualifiedName, string>();
            foreach (OperationDescription operationDescription in contractDescription.Operations)
            {
                DispatchBodyElementAttribute dispatchBodyElement = operationDescription.Behaviors.Find<DispatchBodyElementAttribute>();
                if (dispatchBodyElement != null)
                {
                    dispatchDictionary.Add(dispatchBodyElement.QualifiedName, operationDescription.Name);
                }
            }

            dispatchRuntime.OperationSelector = new DispatchByBodyElementOperationSelector(dispatchDictionary, dispatchRuntime.UnhandledDispatchOperation.Name);
        }
    }
}
