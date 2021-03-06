// <copyright file="SpanHelper.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenTelemetry.Trace
{
    /// <summary>
    /// A collection of helper methods to be used when building spans.
    /// </summary>
    public static class SpanHelper
    {
        /// <summary>
        /// Helper method that populates span properties from http status code according
        /// to https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md#status.
        /// </summary>
        /// <param name="httpStatusCode">Http status code.</param>
        /// <returns>Resolved span <see cref="Status"/> for the Http status code.</returns>
        public static Status ResolveSpanStatusForHttpStatusCode(int httpStatusCode)
        {
            var status = Status.Error;

            if (httpStatusCode >= 100 && httpStatusCode <= 399)
            {
                status = Status.Unset;
            }

            return status;
        }

        /// <summary>
        /// Helper method that populates span properties from RPC status code according
        /// to https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/rpc.md#status.
        /// </summary>
        /// <param name="statusCode">RPC status code.</param>
        /// <returns>Resolved span <see cref="Status"/> for the Grpc status code.</returns>
        public static Status ResolveSpanStatusForGrpcStatusCode(int statusCode)
        {
            var status = Status.Error;

            if (typeof(StatusCanonicalCode).IsEnumDefined(statusCode))
            {
                status = ((StatusCanonicalCode)statusCode) switch
                {
                    StatusCanonicalCode.Ok => Status.Unset,
                    _ => Status.Error,
                };
            }

            return status;
        }
    }
}
