// -----------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static string ToAnonymousParametersString<TResult>(this Expression<Func<TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, TResult>(this Expression<Func<T1, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        public static string ToAnonymousParametersString<T1, T2, T3, T4, T5, T6, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        private static string ReplaceParameters(IEnumerable<ParameterExpression> parameters, string expressionString)
        {
            var i = 1;
            foreach (var parameter in parameters)
            {
                expressionString = expressionString.Replace(parameter.Name, $"arg{i}");
                i++;
            }

            return expressionString;
        }
    }
}