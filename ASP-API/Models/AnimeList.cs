namespace ASP_API.Models
{
    using System;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AnimeList
    {
        [JsonProperty("anime_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? AnimeId { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public AiredString? Title { get; set; }

        [JsonProperty("title_english", NullValueHandling = NullValueHandling.Ignore)]
        public AiredString? TitleEnglish { get; set; }

        [JsonProperty("title_japanese", NullValueHandling = NullValueHandling.Ignore)]
        public AiredString? TitleJapanese { get; set; }

        [JsonProperty("title_synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleSynonyms { get; set; }

        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public Source? Source { get; set; }

        [JsonProperty("episodes", NullValueHandling = NullValueHandling.Ignore)]
        public long? Episodes { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status? Status { get; set; }

        [JsonProperty("airing", NullValueHandling = NullValueHandling.Ignore)]
        public Airing? Airing { get; set; }

        [JsonProperty("aired_string", NullValueHandling = NullValueHandling.Ignore)]
        public AiredString? AiredString { get; set; }

        [JsonProperty("aired", NullValueHandling = NullValueHandling.Ignore)]
        public string Aired { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public string Duration { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public Rating? Rating { get; set; }

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public double? Score { get; set; }

        [JsonProperty("scored_by", NullValueHandling = NullValueHandling.Ignore)]
        public long? ScoredBy { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public AiredString? Rank { get; set; }

        [JsonProperty("popularity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Popularity { get; set; }

        [JsonProperty("members", NullValueHandling = NullValueHandling.Ignore)]
        public long? Members { get; set; }

        [JsonProperty("favorites", NullValueHandling = NullValueHandling.Ignore)]
        public long? Favorites { get; set; }

        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public string Background { get; set; }

        [JsonProperty("premiered", NullValueHandling = NullValueHandling.Ignore)]
        public string Premiered { get; set; }

        [JsonProperty("broadcast", NullValueHandling = NullValueHandling.Ignore)]
        public string Broadcast { get; set; }

        [JsonProperty("related", NullValueHandling = NullValueHandling.Ignore)]
        public EndingTheme? Related { get; set; }

        [JsonProperty("producer", NullValueHandling = NullValueHandling.Ignore)]
        public string Producer { get; set; }

        [JsonProperty("licensor", NullValueHandling = NullValueHandling.Ignore)]
        public string Licensor { get; set; }

        [JsonProperty("studio", NullValueHandling = NullValueHandling.Ignore)]
        public string Studio { get; set; }

        [JsonProperty("genre", NullValueHandling = NullValueHandling.Ignore)]
        public string Genre { get; set; }

        [JsonProperty("opening_theme", NullValueHandling = NullValueHandling.Ignore)]
        public EndingTheme? OpeningTheme { get; set; }

        [JsonProperty("ending_theme", NullValueHandling = NullValueHandling.Ignore)]
        public EndingTheme? EndingTheme { get; set; }
    }

    public enum Airing { False, True };

    public enum Rating { GAllAges, None, Pg13Teens13OrOlder, PgChildren, R17ViolenceProfanity, RMildNudity, RxHentai };

    public enum Source { Book, CardGame, DigitalManga, Game, LightNovel, Manga, Music, Novel, Original, Other, PictureBook, Radio, The4KomaManga, Unknown, VisualNovel, WebManga };

    public enum Status { CurrentlyAiring, FinishedAiring, NotYetAired };

    public enum TypeEnum { Movie, Music, Ona, Ova, Special, Tv, Unknown };

    public partial struct AiredString
    {
        public long? Integer;
        public string String;

        public static implicit operator AiredString(long Integer) => new AiredString { Integer = Integer };
        public static implicit operator AiredString(string String) => new AiredString { String = String };
    }

    public partial struct EndingTheme
    {
        public object[] AnythingArray;
        public string String;

        public static implicit operator EndingTheme(object[] AnythingArray) => new EndingTheme { AnythingArray = AnythingArray };
        public static implicit operator EndingTheme(string String) => new EndingTheme { String = String };
    }

    public partial class AnimeList
    {
        public static AnimeList[] FromJson(string json) => JsonConvert.DeserializeObject<AnimeList[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AnimeList[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AiredStringConverter.Singleton,
                AiringConverter.Singleton,
                EndingThemeConverter.Singleton,
                RatingConverter.Singleton,
                SourceConverter.Singleton,
                StatusConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AiredStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AiredString) || t == typeof(AiredString?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new AiredString { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new AiredString { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type AiredString");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (AiredString)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type AiredString");
        }

        public static readonly AiredStringConverter Singleton = new AiredStringConverter();
    }

    internal class AiringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Airing) || t == typeof(Airing?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "False":
                    return Airing.False;
                case "True":
                    return Airing.True;
            }
            throw new Exception("Cannot unmarshal type Airing");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Airing)untypedValue;
            switch (value)
            {
                case Airing.False:
                    serializer.Serialize(writer, "False");
                    return;
                case Airing.True:
                    serializer.Serialize(writer, "True");
                    return;
            }
            throw new Exception("Cannot marshal type Airing");
        }

        public static readonly AiringConverter Singleton = new AiringConverter();
    }

    internal class EndingThemeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EndingTheme) || t == typeof(EndingTheme?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new EndingTheme { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<object[]>(reader);
                    return new EndingTheme { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type EndingTheme");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (EndingTheme)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            throw new Exception("Cannot marshal type EndingTheme");
        }

        public static readonly EndingThemeConverter Singleton = new EndingThemeConverter();
    }

    internal class RatingConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rating) || t == typeof(Rating?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "G - All Ages":
                    return Rating.GAllAges;
                case "None":
                    return Rating.None;
                case "PG - Children":
                    return Rating.PgChildren;
                case "PG-13 - Teens 13 or older":
                    return Rating.Pg13Teens13OrOlder;
                case "R - 17+ (violence & profanity)":
                    return Rating.R17ViolenceProfanity;
                case "R+ - Mild Nudity":
                    return Rating.RMildNudity;
                case "Rx - Hentai":
                    return Rating.RxHentai;
            }
            throw new Exception("Cannot unmarshal type Rating");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Rating)untypedValue;
            switch (value)
            {
                case Rating.GAllAges:
                    serializer.Serialize(writer, "G - All Ages");
                    return;
                case Rating.None:
                    serializer.Serialize(writer, "None");
                    return;
                case Rating.PgChildren:
                    serializer.Serialize(writer, "PG - Children");
                    return;
                case Rating.Pg13Teens13OrOlder:
                    serializer.Serialize(writer, "PG-13 - Teens 13 or older");
                    return;
                case Rating.R17ViolenceProfanity:
                    serializer.Serialize(writer, "R - 17+ (violence & profanity)");
                    return;
                case Rating.RMildNudity:
                    serializer.Serialize(writer, "R+ - Mild Nudity");
                    return;
                case Rating.RxHentai:
                    serializer.Serialize(writer, "Rx - Hentai");
                    return;
            }
            throw new Exception("Cannot marshal type Rating");
        }

        public static readonly RatingConverter Singleton = new RatingConverter();
    }

    internal class SourceConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Source) || t == typeof(Source?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "4-koma manga":
                    return Source.The4KomaManga;
                case "Book":
                    return Source.Book;
                case "Card game":
                    return Source.CardGame;
                case "Digital manga":
                    return Source.DigitalManga;
                case "Game":
                    return Source.Game;
                case "Light novel":
                    return Source.LightNovel;
                case "Manga":
                    return Source.Manga;
                case "Music":
                    return Source.Music;
                case "Novel":
                    return Source.Novel;
                case "Original":
                    return Source.Original;
                case "Other":
                    return Source.Other;
                case "Picture book":
                    return Source.PictureBook;
                case "Radio":
                    return Source.Radio;
                case "Unknown":
                    return Source.Unknown;
                case "Visual novel":
                    return Source.VisualNovel;
                case "Web manga":
                    return Source.WebManga;
            }
            throw new Exception("Cannot unmarshal type Source");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Source)untypedValue;
            switch (value)
            {
                case Source.The4KomaManga:
                    serializer.Serialize(writer, "4-koma manga");
                    return;
                case Source.Book:
                    serializer.Serialize(writer, "Book");
                    return;
                case Source.CardGame:
                    serializer.Serialize(writer, "Card game");
                    return;
                case Source.DigitalManga:
                    serializer.Serialize(writer, "Digital manga");
                    return;
                case Source.Game:
                    serializer.Serialize(writer, "Game");
                    return;
                case Source.LightNovel:
                    serializer.Serialize(writer, "Light novel");
                    return;
                case Source.Manga:
                    serializer.Serialize(writer, "Manga");
                    return;
                case Source.Music:
                    serializer.Serialize(writer, "Music");
                    return;
                case Source.Novel:
                    serializer.Serialize(writer, "Novel");
                    return;
                case Source.Original:
                    serializer.Serialize(writer, "Original");
                    return;
                case Source.Other:
                    serializer.Serialize(writer, "Other");
                    return;
                case Source.PictureBook:
                    serializer.Serialize(writer, "Picture book");
                    return;
                case Source.Radio:
                    serializer.Serialize(writer, "Radio");
                    return;
                case Source.Unknown:
                    serializer.Serialize(writer, "Unknown");
                    return;
                case Source.VisualNovel:
                    serializer.Serialize(writer, "Visual novel");
                    return;
                case Source.WebManga:
                    serializer.Serialize(writer, "Web manga");
                    return;
            }
            throw new Exception("Cannot marshal type Source");
        }

        public static readonly SourceConverter Singleton = new SourceConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Currently Airing":
                    return Status.CurrentlyAiring;
                case "Finished Airing":
                    return Status.FinishedAiring;
                case "Not yet aired":
                    return Status.NotYetAired;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.CurrentlyAiring:
                    serializer.Serialize(writer, "Currently Airing");
                    return;
                case Status.FinishedAiring:
                    serializer.Serialize(writer, "Finished Airing");
                    return;
                case Status.NotYetAired:
                    serializer.Serialize(writer, "Not yet aired");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Movie":
                    return TypeEnum.Movie;
                case "Music":
                    return TypeEnum.Music;
                case "ONA":
                    return TypeEnum.Ona;
                case "OVA":
                    return TypeEnum.Ova;
                case "Special":
                    return TypeEnum.Special;
                case "TV":
                    return TypeEnum.Tv;
                case "Unknown":
                    return TypeEnum.Unknown;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Movie:
                    serializer.Serialize(writer, "Movie");
                    return;
                case TypeEnum.Music:
                    serializer.Serialize(writer, "Music");
                    return;
                case TypeEnum.Ona:
                    serializer.Serialize(writer, "ONA");
                    return;
                case TypeEnum.Ova:
                    serializer.Serialize(writer, "OVA");
                    return;
                case TypeEnum.Special:
                    serializer.Serialize(writer, "Special");
                    return;
                case TypeEnum.Tv:
                    serializer.Serialize(writer, "TV");
                    return;
                case TypeEnum.Unknown:
                    serializer.Serialize(writer, "Unknown");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
