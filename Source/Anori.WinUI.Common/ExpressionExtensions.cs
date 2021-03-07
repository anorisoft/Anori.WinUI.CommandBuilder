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

    /// <summary>
    ///     Expression Extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<TResult>(this Expression<Func<TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, TResult>(this Expression<Func<T1, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Converts to anonymousparametersstring.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Result of ToAnonymousParametersString as String.
        /// </returns>
        public static string ToAnonymousParametersString<T1, T2, T3, T4, T5, T6, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) =>
            ReplaceParameters(expression.Parameters, expression.ToString());

        /// <summary>
        ///     Replaces the parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expressionString">The expression string.</param>
        /// <returns>
        ///     Result of ReplaceParameters as String.
        /// </returns>
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