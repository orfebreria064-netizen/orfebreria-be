using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AURUM.WebAPI.Models
{
    // Las columnas DATE de SQL Server llegan como DateTime a medianoche: se escriben como "yyyy-MM-dd"
    // (igual que Supabase), para que el frontend no reciba una hora que no existe.
    public class FechaJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.Parse(reader.GetString()!, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.TimeOfDay == TimeSpan.Zero && value.Kind == DateTimeKind.Unspecified)
                writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            else
                writer.WriteStringValue(value.ToString("O", CultureInfo.InvariantCulture));
        }
    }
}
