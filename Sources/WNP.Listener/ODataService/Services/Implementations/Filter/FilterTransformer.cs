// <copyright file="FilterTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.OData.Query;
    using MetadataService;
    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using Microsoft.OData.Edm;
    using Services.Filter;
    using Utilities;

    /// <summary>
    /// Implements <see cref="IFilterTransformer"/> interface.
    /// </summary>
    public class FilterTransformer : IFilterTransformer
    {
        private static readonly WhereClause EmptyWhereClause = new WhereClause
        {
            Clause = string.Empty,
            PositionalParameters = new object[0]
        };

        private readonly IMetadataProvider metadataService;
        private readonly IODataFunctionToSqlConvertor oDataToSql;

        private readonly List<object> positionalParmeters = new List<object>();
        private int positionalArgsOffset = 0;

        private FilterQueryOption filterQueryOption;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterTransformer"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="oDataToSql">The OData to SQL converter.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Justification = "FxCop bug.")]
        public FilterTransformer(IMetadataProvider metadataService, IODataFunctionToSqlConvertor oDataToSql)
        {
            this.metadataService = metadataService;
            this.oDataToSql = oDataToSql;
        }

        /// <inheritdoc/>
        public WhereClause TransformFilterQueryOption(FilterQueryOption newFilterQueryOption)
        {
            return this.TransformFilterQueryOption(newFilterQueryOption, 0);
        }

        /// <inheritdoc/>
        public WhereClause TransformFilterQueryOption(FilterQueryOption newFilterQueryOption, int newPositionalArgsOffset)
        {
            this.positionalArgsOffset = newPositionalArgsOffset;

            if ((this.filterQueryOption = newFilterQueryOption) == null)
            {
                return EmptyWhereClause;
            }

            return new WhereClause
            {
                Clause = StringUtilities.Invariant($"{this.BindFilter(newFilterQueryOption)}{Environment.NewLine}"),
                PositionalParameters = this.positionalParmeters.ToArray()
            };
        }

        private static string BindRangeVariable(NonentityRangeVariable nonentityRangeVariable) =>
            nonentityRangeVariable.Name;

        private static string BindRangeVariable(EntityRangeVariable entityRangeVariable) =>
            entityRangeVariable.Name;

        private string BindFilter(FilterQueryOption filterQuery) =>
                    this.BindFilterClause(filterQuery.FilterClause);

        private string BindFilterClause(FilterClause filterClause) =>
            this.Bind(filterClause.Expression);

        private string Bind(QueryNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }

            var collectionNode = node as CollectionNode;
            var singleValueNode = node as SingleValueNode;

            if (collectionNode != null)
            {
                switch (node.Kind)
                {
                    case QueryNodeKind.CollectionNavigationNode:
                        var navigationNode = node as CollectionNavigationNode;
                        return this.BindNavigationPropertyNode(navigationNode.Source, navigationNode.NavigationProperty);

                    case QueryNodeKind.CollectionPropertyAccess:
                        return this.BindCollectionPropertyAccessNode(node as CollectionPropertyAccessNode);
                }
            }
            else if (singleValueNode != null)
            {
                switch (node.Kind)
                {
                    case QueryNodeKind.BinaryOperator:
                        return this.BindBinaryOperatorNode(node as BinaryOperatorNode);

                    case QueryNodeKind.Constant:
                        return this.BindConstantNode(node as ConstantNode);

                    case QueryNodeKind.Convert:
                        return this.BindConvertNode(node as ConvertNode);

                    case QueryNodeKind.EntityRangeVariableReference:
                        return BindRangeVariable((node as EntityRangeVariableReferenceNode).RangeVariable);

                    case QueryNodeKind.NonentityRangeVariableReference:
                        return BindRangeVariable((node as NonentityRangeVariableReferenceNode).RangeVariable);

                    case QueryNodeKind.SingleValuePropertyAccess:
                        return this.BindPropertyAccessQueryNode(node as SingleValuePropertyAccessNode);

                    case QueryNodeKind.UnaryOperator:
                        return this.BindUnaryOperatorNode(node as UnaryOperatorNode);

                    case QueryNodeKind.SingleValueFunctionCall:
                        return this.BindSingleValueFunctionCallNode(node as SingleValueFunctionCallNode);

                    case QueryNodeKind.SingleNavigationNode:
                        var navigationNode = node as SingleNavigationNode;
                        return this.BindNavigationPropertyNode(navigationNode.Source, navigationNode.NavigationProperty);

                    case QueryNodeKind.Any:
                        return this.BindAnyNode(node as AnyNode);

                    case QueryNodeKind.All:
                        return this.BindAllNode(node as AllNode);
                }
            }

            throw new NotSupportedException(StringUtilities.Invariant($"Nodes of type {node.Kind} are not supported"));
        }

        private string BindCollectionPropertyAccessNode(CollectionPropertyAccessNode collectionPropertyAccessNode)
        {
            var source = this.Bind(collectionPropertyAccessNode.Source);
            return StringUtilities.Invariant($"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, collectionPropertyAccessNode.Property.Name)}");
        }

        private string BindNavigationPropertyNode(SingleValueNode singleValueNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = this.Bind(singleValueNode);
            return StringUtilities.Invariant($"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, edmNavigationProperty.Name)}");
        }

        private string BindAllNode(AllNode allNode) =>
            StringUtilities.Invariant($@"
NOT EXISTS (
    FROM {this.Bind(allNode.Source)} {allNode.RangeVariables.First().Name}
    WHERE NOT({this.Bind(allNode.Body)})
)");

        private string BindAnyNode(AnyNode anyNode) =>
            StringUtilities.Invariant($@"
EXISTS (
    FROM {this.Bind(anyNode.Source)} {anyNode.RangeVariables.First().Name}
    {this.AddWhereIfNotNull(anyNode.Body)}
)");

        private string AddWhereIfNotNull(SingleValueNode node) =>
            node != null ? StringUtilities.Invariant($"WHERE {this.Bind(node)}") : string.Empty;

        private string BindNavigationPropertyNode(SingleEntityNode singleEntityNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = this.Bind(singleEntityNode);
            return StringUtilities.Invariant($"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, edmNavigationProperty.Name)}");
        }

        private string BindSingleValueFunctionCallNode(SingleValueFunctionCallNode singleValueFunctionCallNode)
        {
            var arguments = singleValueFunctionCallNode.Parameters.ToList();
            var funName = singleValueFunctionCallNode.Name;

            if (!this.oDataToSql.IsSupported(funName))
            {
                throw new NotSupportedException(StringUtilities.Invariant($"{funName} is not supported"));
            }

            return this.oDataToSql.Convert(funName, this.Bind, arguments);
        }

        private string BindUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode) =>
            StringUtilities.Invariant($"{unaryOperatorNode.OperatorKind.ToSqlOperator()}({this.Bind(unaryOperatorNode.Operand)})");

        private string BindPropertyAccessQueryNode(SingleValuePropertyAccessNode singleValuePropertyAccessNode)
        {
            var propertyName = singleValuePropertyAccessNode.Property.Name;
            var source = this.Bind(singleValuePropertyAccessNode.Source);

            return StringUtilities.Invariant($"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, propertyName)}");
        }

        private string BindConvertNode(ConvertNode convertNode) =>
            this.Bind(convertNode.Source);

        private string BindConstantNode(ConstantNode constantNode)
        {
            this.positionalParmeters.Add(constantNode.ToKnownClrType());

            return StringUtilities.Invariant($"@{this.positionalParmeters.Count - 1 + this.positionalArgsOffset}");
        }

        private string BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode)
        {
            var left = this.Bind(binaryOperatorNode.Left);
            var right = this.Bind(binaryOperatorNode.Right);

            return StringUtilities.Invariant($"({left} {binaryOperatorNode.OperatorKind.ToSqlOperator()} {right})");
        }

        private string VariableToTable(string rangeVariable)
        {
            var actualVar = this.VariableToActualName(rangeVariable);
            return this.metadataService.GetModelMapping(actualVar).TableName;
        }

        private string PropertyNameToColumn(string variable, string propertyName)
        {
            var actualVar = this.VariableToActualName(variable);
            var modelMapping = this.metadataService.GetModelMapping(actualVar);

            return modelMapping.ModelToColumnMappings[propertyName];
        }

        private string VariableToActualName(string rangeVariable)
        {
            if (string.Equals(rangeVariable, "$it", StringComparison.OrdinalIgnoreCase))
            {
                return this.filterQueryOption.Context.ElementClrType.Name;
            }

            return rangeVariable;
        }
    }
}