using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IdentityServer.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; }

        public static Response<T> Success(T Data, int StatusCode)
        {
            return new Response<T> { Data = Data, StatusCode = StatusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int StatusCode)
        {
            return new Response<T> { Data = default, StatusCode = StatusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)

        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}