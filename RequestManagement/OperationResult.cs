﻿using System;
using System.Collections.Generic;
using System.Net;

namespace RequestManagement
{
    /// <summary>
    /// Operation Result
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful
        /// </summary>
        public bool IsSuccess { get; protected set; }

        /// <summary>
        /// Gets or sets the HTTP status
        /// </summary>
        public HttpStatusCode Status { get; protected set; }

        /// <summary>
        /// Gets or sets a the operation errors
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Errors { get; protected set; }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.OK"/> <see cref="OperationResult"/>
        /// </summary>
        /// <returns>A <see cref="HttpStatusCode.OK"/> <see cref="OperationResult"/></returns>
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                IsSuccess = true,
                Status = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.OK"/> <see cref="OperationResult{T}"/> with data
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data</param>
        /// <returns>A <see cref="HttpStatusCode.OK"/> <see cref="OperationResult{T}"/> with data</returns>
        public static OperationResult<T> Success<T>(T data)
        {
            return new OperationResult<T>(data)
            {
                IsSuccess = true,
                Status = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult"/> with a set of errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult"/></returns>
        public static OperationResult Fail(IDictionary<string, IEnumerable<string>> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return new OperationResult()
            {
                IsSuccess = false,
                Status = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult{T}"/> with a set of errors
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult{T}"/> containing no data</returns>
        public static OperationResult<T> Fail<T>(IDictionary<string, IEnumerable<string>> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return new OperationResult<T>(default(T))
            {
                IsSuccess = false,
                Status = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult"/> with a single non-field-specific error
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult"/></returns>
        public static OperationResult Fail(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            var errors = new Dictionary<string, IEnumerable<string>>()
            {
                { string.Empty, new string[] { errorMessage } }
            };

            return Fail(errors);
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult{T}"/> with a single non-field-specific error
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="errorMessage">Error message</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="OperationResult{T}"/> containing no data</returns>
        public static OperationResult<T> Fail<T>(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            var errors = new Dictionary<string, IEnumerable<string>>()
            {
                { string.Empty, new string[] { errorMessage } }
            };

            return Fail<T>(errors);
        }

        /// <summary>
        /// Gets a <see cref="HttpStatusCode.NotFound"/> failure <see cref="OperationResult{T}"/>
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>A <see cref="HttpStatusCode.NotFound"/> <see cref="OperationResult{T}"/></returns>
        public static OperationResult<T> NotFound<T>()
        {
            return new OperationResult<T>(default(T))
            {
                IsSuccess = false,
                Status = HttpStatusCode.NotFound
            };
        }
    }

#pragma warning disable SA1402 // File may only contain a single class
    /// <summary>
    /// Operation Result
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class OperationResult<T> : OperationResult
#pragma warning restore SA1402 // File may only contain a single class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult{T}"/> class
        /// </summary>
        /// <param name="data">Data</param>
        internal OperationResult(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets the operation data
        /// </summary>
        public T Data { get; protected set; }
    }
}
