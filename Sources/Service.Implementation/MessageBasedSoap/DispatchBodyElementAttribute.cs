//-----------------------------------------------------------------------
// <copyright file="DispatchBodyElementAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.MessageBasedSoap
{
    using System;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using System.Xml;

    /// <summary>
    /// While this class is also a behavior, it is passive and does not actively contribute any configuration changes to the dispatch runtime. 
    /// All of its methods return to the caller without taking any actions. 
    /// The operation behavior only exists so that the metadata required for the new dispatch mechanism, namely the qualified name of 
    /// the body element on whose occurrence an operation is selected, can be associated with the respective operations.
    /// <see href="http://msdn.microsoft.com/en-us/library/aa395223(v=vs.90).aspx">Initial source</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DispatchBodyElementAttribute : Attribute, IOperationBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchBodyElementAttribute"/> class.
        /// </summary>
        /// <param name="bodyElement">The body element.</param>
        /// <param name="ns">The namespace.</param>
        public DispatchBodyElementAttribute(string bodyElement, string ns)
        {
            this.BodyElement = bodyElement;
            this.NS = ns;
            this.QualifiedName = new XmlQualifiedName(this.BodyElement, this.NS);
        }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        /// <value>
        /// The body element.
        /// </value>
        public string BodyElement { get; private set; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string NS { get; private set; }

        /// <summary>
        /// Gets the qualified name.
        /// </summary>
        /// <value>
        /// The qualified name.
        /// </value>
        public XmlQualifiedName QualifiedName { get; private set; }

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="bindingParameters">The collection of objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="clientOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription" />.</param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation) 
        { 
        }

        /// <summary>
        /// Implements a modification or extension of the service across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="dispatchOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription" />.</param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation) 
        { 
        }

        /// <summary>
        /// Implement to confirm that the operation meets some intended criteria.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        public void Validate(OperationDescription operationDescription) 
        { 
        }
    }
}
