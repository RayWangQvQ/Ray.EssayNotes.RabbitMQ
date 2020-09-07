﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ray.Infrastructure.Helpers;

namespace Ray.Infrastructure.Extensions.Linq
{
    public static class QueryableExtension
    {
        public static IQueryable<TAggregateRoot> Set<TAggregateRoot>(this IQueryable<TAggregateRoot> tAggregateRoots,
            Expression<Func<TAggregateRoot, TAggregateRoot>> tAggregateRoot)
            where TAggregateRoot : class, new()
        {
            if (tAggregateRoot != null)
            {
                var exp = tAggregateRoot.Body as MemberInitExpression;

                if (exp == null || exp.Bindings.Count == 0)
                    throw new Exception("无效的Lambda表达式");

                foreach (var binding in exp.Bindings)
                {
                    var ext = new EmitExtension<TAggregateRoot>();
                    ext.BuildEmitMethod(typeof(TAggregateRoot), binding.Member.Name);

                    var memberAssignment = binding as MemberAssignment;
                    if (memberAssignment == null)
                        throw new ArgumentException("表达式必须为MemberAssignment");

                    Expression memberExpression = memberAssignment.Expression;

                    ParameterExpression parameterExpression = null;

                    memberExpression.Visit((ParameterExpression p) =>
                    {
                        parameterExpression = p;
                        return p;
                    });

                    object value = null;

                    if (parameterExpression == null)
                    {
                        if (memberExpression.NodeType == ExpressionType.Constant)
                        {
                            var constantExpression = memberExpression as ConstantExpression;
                            if (constantExpression == null)
                                throw new ArgumentException(
                                    "MemberAssignment expression 无效");
                            value = constantExpression.Value;
                        }
                        else
                        {
                            var lambda = Expression.Lambda(memberExpression, null);
                            value = lambda.Compile().DynamicInvoke();
                        }
                    }
                    else
                    {
                        throw new Exception("无效的Lambda表达式");
                    }
                    tAggregateRoots.ToList().ForEach(x => ext.EmitSetValue(x, value));
                }
            }
            else
            {
                throw new Exception("无效的Lambda表达式");
            }
            return tAggregateRoots;
        }

        public static IQueryable<TAggregateRoot> Set<TAggregateRoot, TField>(this IQueryable<TAggregateRoot> item,
            Expression<Func<TAggregateRoot, TField>> field, TField value) where TAggregateRoot : class, new()
        {
            if (field != null)
            {
                var exp = field.Body as MemberExpression;

                if (exp == null || field.Parameters.Count == 0)
                {
                    throw new Exception("无效的Lambda表达式");
                }

                var ext = new EmitExtension<TAggregateRoot>();

                ext.BuildEmitMethod(typeof(TAggregateRoot), exp.Member.Name);
                item.ToList().ForEach(x => ext.EmitSetValue(x, value));
            }
            else
            {
                throw new Exception("无效的Lambda表达式");
            }

            return item;
        }

        public static TAggregateRoot Set<TAggregateRoot>(this TAggregateRoot item,
            Expression<Func<TAggregateRoot>> field)
        {
            return default(TAggregateRoot);
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            CheckHelper.NotNull(query, nameof(query));

            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// IQueryable分页
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            CheckHelper.NotNull(query, nameof(query));

            return query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
