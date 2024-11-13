using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TapHoa.Helpers
{
    public static class ExtensionHelper
    {
        // Phương thức mở rộng cho định dạng tiền VND
        public static string ToVnd(this double giaTri)
        {
            return $"{giaTri:#,##0.00} đ";
        }
    }

    public static class SessionExtensions
    {
        // Phương thức Set cho session với kiểu dữ liệu chung T
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Phương thức Get cho session với kiểu dữ liệu chung T
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}