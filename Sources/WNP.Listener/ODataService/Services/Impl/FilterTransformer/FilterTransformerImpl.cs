using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.OData.Query;
using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.ODataService.Services.FilterTransformer;
using AMSLLC.Listener.Utilities;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace AMSLLC.Listener.ODataService.Services.Impl.FilterTransformer
{
    public class FilterTransformerImpl : IFilterTransformer
    {
        private readonly IMetadataService _metadataService;
        private readonly IODataFunctionToSqlConvertor _oDataToSql;

        private readonly List<object> _positionalParmeters = new List<object>();
        private static readonly WhereClause EmptyWhereClause = new WhereClause
        {
            Clause = string.Empty,
            PositionalParameters = new object[0]
        };

        private FilterQueryOption _filterQueryOption;

        public FilterTransformerImpl(IMetadataService metadataService, IODataFunctionToSqlConvertor ioDataToSql)
        {
            _metadataService = metadataService;
            _oDataToSql = ioDataToSql;
        }

        public WhereClause TransformFilterQueryOption(FilterQueryOption filterQueryOption)
        {
            if ((_filterQueryOption = filterQueryOption) == null)
                return EmptyWhereClause;

            return new WhereClause
            {
                Clause = $"{BindFilter(filterQueryOption)}{Environment.NewLine}",
                PositionalParameters = _positionalParmeters.ToArray()
            };
        }

        protected string BindFilter(FilterQueryOption filterQuery) =>
            BindFilterClause(filterQuery.FilterClause);

        protected string BindFilterClause(FilterClause filterClause) =>
            Bind(filterClause.Expression);

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
                        return BindNavigationPropertyNode(navigationNode.Source, navigationNode.NavigationProperty);

                    case QueryNodeKind.CollectionPropertyAccess:
                        return BindCollectionPropertyAccessNode(node as CollectionPropertyAccessNode);
                }
            }
            else if (singleValueNode != null)
            {
                switch (node.Kind)
                {
                    case QueryNodeKind.BinaryOperator:
                        return BindBinaryOperatorNode(node as BinaryOperatorNode);

                    case QueryNodeKind.Constant:
                        return BindConstantNode(node as ConstantNode);

                    case QueryNodeKind.Convert:
                        return BindConvertNode(node as ConvertNode);

                    case QueryNodeKind.EntityRangeVariableReference:
                        return BindRangeVariable((node as EntityRangeVariableReferenceNode).RangeVariable);

                    case QueryNodeKind.NonentityRangeVariableReference:
                        return BindRangeVariable((node as NonentityRangeVariableReferenceNode).RangeVariable);

                    case QueryNodeKind.SingleValuePropertyAccess:
                        return BindPropertyAccessQueryNode(node as SingleValuePropertyAccessNode);

                    case QueryNodeKind.UnaryOperator:
                        return BindUnaryOperatorNode(node as UnaryOperatorNode);

                    case QueryNodeKind.SingleValueFunctionCall:
                        return BindSingleValueFunctionCallNode(node as SingleValueFunctionCallNode);

                    case QueryNodeKind.SingleNavigationNode:
                        var navigationNode = node as SingleNavigationNode;
                        return BindNavigationPropertyNode(navigationNode.Source, navigationNode.NavigationProperty);

                    case QueryNodeKind.Any:
                        return BindAnyNode(node as AnyNode);

                    case QueryNodeKind.All:
                        return BindAllNode(node as AllNode);
                }
            }

            throw new NotSupportedException($"Nodes of type {node.Kind} are not supported");
        }

        private string BindCollectionPropertyAccessNode(CollectionPropertyAccessNode collectionPropertyAccessNode)
        {
            var source = Bind(collectionPropertyAccessNode.Source);
            return $"{VariableToTable(source)}.{PropertyNameToColumn(source, collectionPropertyAccessNode.Property.Name)}";
        }

        private string BindNavigationPropertyNode(SingleValueNode singleValueNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = Bind(singleValueNode);
            return $"{VariableToTable(source)}.{PropertyNameToColumn(source, edmNavigationProperty.Name)}";
        }

        private string BindAllNode(AllNode allNode) =>
            "NOT EXISTS (" +
            $" FROM {Bind(allNode.Source)} {allNode.RangeVariables.First().Name}" +
            $" WHERE NOT({Bind(allNode.Body)})" +
            ")";

        private string BindAnyNode(AnyNode anyNode) =>
            "EXISTS (" +
                $" FROM {Bind(anyNode.Source)} {anyNode.RangeVariables.First().Name}" +
            (anyNode.Body != null ?
                $" WHERE {Bind(anyNode.Body)}" : "") +
            ")";

        private string BindNavigationPropertyNode(SingleEntityNode singleEntityNode, IEdmNavigationProperty edmNavigationProperty)
        {
            var source = Bind(singleEntityNode);
            return $"{VariableToTable(source)}.{PropertyNameToColumn(source, edmNavigationProperty.Name)}";
        }

        private string BindSingleValueFunctionCallNode(SingleValueFunctionCallNode singleValueFunctionCallNode)
        {
            var arguments = singleValueFunctionCallNode.Parameters.ToList();
            var funName = singleValueFunctionCallNode.Name.ToLowerInvariant();

            if (!_oDataToSql.IsSupported(funName))
                throw new NotSupportedException($"{funName} is not supported");

            return _oDataToSql.Convert(funName, Bind, arguments);
        }

        private string BindUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode) =>
            $"{unaryOperatorNode.OperatorKind.ToSqlOperator()}({Bind(unaryOperatorNode.Operand)})";

        private string BindPropertyAccessQueryNode(SingleValuePropertyAccessNode singleValuePropertyAccessNode)
        {
            var propertyName = singleValuePropertyAccessNode.Property.Name;
            var source = Bind(singleValuePropertyAccessNode.Source);

            return $"{VariableToTable(source)}.{PropertyNameToColumn(source, propertyName)}";
        }

        private string BindRangeVariable(NonentityRangeVariable nonentityRangeVariable) =>
            nonentityRangeVariable.Name;

        private string BindRangeVariable(EntityRangeVariable entityRangeVariable) =>
            entityRangeVariable.Name;

        private string BindConvertNode(ConvertNode convertNode) =>
            Bind(convertNode.Source);

        private string BindConstantNode(ConstantNode constantNode)
        {
            _positionalParmeters.Add(constantNode.ToKnownClrType());

            return $"@{_positionalParmeters.Count - 1}";
        }

        private string BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode)
        {
            var left = Bind(binaryOperatorNode.Left);
            var right = Bind(binaryOperatorNode.Right);

            return $"({left} {binaryOperatorNode.OperatorKind.ToSqlOperator()} {right})";
        }

        private string VariableToTable(string rangeVariable)
        {
            var actualVar = VariableToActualName(rangeVariable);
            return _metadataService.GetModelMapping(actualVar).TableName;
        }

        private string PropertyNameToColumn(string variable, string propertyName)
        {
            var actualVar = VariableToActualName(variable);
            var modelMapping = _metadataService.GetModelMapping(actualVar);

            return modelMapping.ModelToColumnMappings[propertyName];
        }

        private string VariableToActualName(string rangeVariable)
        {
            if (string.Equals(rangeVariable, "$it", StringComparison.OrdinalIgnoreCase))
                return _filterQueryOption.Context.ElementClrType.Name;

            return rangeVariable;
        }
    }
}