// <copyright file="FilterTransformerImpl.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.FilterTransformer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.OData.Query;
    using AMSLLC.Listener.MetadataService;
    using AMSLLC.Listener.ODataService.Services.FilterTransformer;
    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using Microsoft.OData.Edm;

    public class FilterTransformerImpl : IFilterTransformer
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

        public FilterTransformerImpl(IMetadataProvider metadataService, IODataFunctionToSqlConvertor ioDataToSql)
        {
            this.metadataService = metadataService;
            this.oDataToSql = ioDataToSql;
        }

        /// <inheritdoc/>
        public WhereClause TransformFilterQueryOption(FilterQueryOption filterQueryOption, int positionalArgsOffset = 0)
        {
            this.positionalArgsOffset = positionalArgsOffset;

            if ((this.filterQueryOption = filterQueryOption) == null)
            {
                return EmptyWhereClause;
            }

            return new WhereClause
            {
                Clause = $"{this.BindFilter(filterQueryOption)}{Environment.NewLine}",
                PositionalParameters = this.positionalParmeters.ToArray()
            };
        }

        protected string BindFilter(FilterQueryOption filterQuery) =>
            this.BindFilterClause(filterQuery.FilterClause);

        protected string BindFilterClause(FilterClause filterClause) =>
            this.Bind(filterClause.Expression);

        protected string Bind(QueryNode node)
        {
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
                        return this.BindRangeVariable((node as EntityRangeVariableReferenceNode).RangeVariable);

                    case QueryNodeKind.NonentityRangeVariableReference:
                        return this.BindRangeVariable((node as NonentityRangeVariableReferenceNode).RangeVariable);

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

            throw new NotSupportedException($"Nodes of type {node.Kind} are not supported");
        }

        private string BindCollectionPropertyAccessNode(CollectionPropertyAccessNode collectionPropertyAccessNode)
        {
            var source = this.Bind(collectionPropertyAccessNode.Source);
            return $"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, collectionPropertyAccessNode.Property.Name)}";
        }

        private string BindNavigationPropertyNode(SingleValueNode singleValueNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = this.Bind(singleValueNode);
            return $"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, edmNavigationProperty.Name)}";
        }

        private string BindAllNode(AllNode allNode) =>
            "NOT EXISTS (" +
            $" FROM {this.Bind(allNode.Source)} {allNode.RangeVariables.First().Name}" +
            $" WHERE NOT({this.Bind(allNode.Body)})" +
            ")";

        private string BindAnyNode(AnyNode anyNode) =>
            "EXISTS (" +
                $" FROM {this.Bind(anyNode.Source)} {anyNode.RangeVariables.First().Name}" +
            (anyNode.Body != null ?
                $" WHERE {this.Bind(anyNode.Body)}" : string.Empty) +
            ")";

        private string BindNavigationPropertyNode(SingleEntityNode singleEntityNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = this.Bind(singleEntityNode);
            return $"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, edmNavigationProperty.Name)}";
        }

        private string BindSingleValueFunctionCallNode(SingleValueFunctionCallNode singleValueFunctionCallNode)
        {
            var arguments = singleValueFunctionCallNode.Parameters.ToList();
            var funName = singleValueFunctionCallNode.Name.ToLowerInvariant();

            if (!this.oDataToSql.IsSupported(funName))
            {
                throw new NotSupportedException($"{funName} is not supported");
            }

            return this.oDataToSql.Convert(funName, this.Bind, arguments);
        }

        private string BindUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode) =>
            $"{unaryOperatorNode.OperatorKind.ToSqlOperator()}({this.Bind(unaryOperatorNode.Operand)})";

        private string BindPropertyAccessQueryNode(SingleValuePropertyAccessNode singleValuePropertyAccessNode)
        {
            var propertyName = singleValuePropertyAccessNode.Property.Name;
            var source = this.Bind(singleValuePropertyAccessNode.Source);

            return $"{this.VariableToTable(source)}.{this.PropertyNameToColumn(source, propertyName)}";
        }

        private string BindRangeVariable(NonentityRangeVariable nonentityRangeVariable) =>
            nonentityRangeVariable.Name;

        private string BindRangeVariable(EntityRangeVariable entityRangeVariable) =>
            entityRangeVariable.Name;

        private string BindConvertNode(ConvertNode convertNode) =>
            this.Bind(convertNode.Source);

        private string BindConstantNode(ConstantNode constantNode)
        {
            this.positionalParmeters.Add(constantNode.ToKnownClrType());

            return $"@{this.positionalParmeters.Count - 1 + this.positionalArgsOffset}";
        }

        private string BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode)
        {
            var left = this.Bind(binaryOperatorNode.Left);
            var right = this.Bind(binaryOperatorNode.Right);

            return $"({left} {binaryOperatorNode.OperatorKind.ToSqlOperator()} {right})";
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