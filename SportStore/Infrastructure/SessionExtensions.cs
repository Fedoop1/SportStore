using System.Text.Json;
using Microsoft.AspNetCore.Http;
using static System.Text.Json.JsonSerializer;

namespace SportStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value) => session.SetString(key, JsonSerializer.Serialize(value));

        public static TResult GetJson<TResult>(this ISession session, string key)
        {
            if (session.GetString("cart") is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<TResult>(session.GetString(key));
        }
    }
}
