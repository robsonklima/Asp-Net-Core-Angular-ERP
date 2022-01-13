using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SAT.MODELS.Entities
{
    public enum Status
    {
        Undefined = 0,

        [EnumMember(Value = "OK")]
        Ok,

        [EnumMember(Value = "ZERO_RESULTS")]
        ZeroResults,

        [EnumMember(Value = "OVER_QUERY_LIMIT")]
        OverQueryLimit,

        [EnumMember(Value = "REQUEST_DENIED")]
        RequestDenied,

        [EnumMember(Value = "INVALID_REQUEST")]
        InvalidRequest,

        [EnumMember(Value = "MAX_ELEMENTS_EXCEEDED")]
        MaxElementsExceeded,

        [EnumMember(Value = "MAX_WAYPOINTS_EXCEEDED")]
        MaxWaypointsExceeded,

        [EnumMember(Value = "MAX_ROUTE_LENGTH_EXCEEDED")]
        MaxRouteLengthExceeded,

        [EnumMember(Value = "NOT_FOUND")]
        NotFound,

        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError,

        [EnumMember(Value = "HTTP_ERROR")]
        HttpError,

        [EnumMember(Value = "NO_API_KEY")]
        InvalidKey
    }

    public class Duration
    {
        public virtual string Text { get; set; }
        public virtual int Value { get; set; }
        public virtual string TimeZone { get; set; }
    }

    public class Distance
    {
        public virtual int Value { get; set; }
        public virtual string Text { get; set; }
    }

    public class Fare
    {
        public virtual string Currency { get; set; }
        public double? Value { get; set; }
        public virtual string Text { get; set; }
    }

    public class Element
    {

        public virtual Status Status { get; set; }
        public virtual Duration Duration { get; set; }
        public virtual Duration DurationInTraffic { get; set; }
        public virtual Distance Distance { get; set; }
        public virtual Fare Fare { get; set; }
    }

    public class Row
    {
        public virtual IEnumerable<Element> Elements { get; set; }
    }

    public class DistanceMatrixResponse
    {
        public virtual IEnumerable<string> OriginAddresses { get; set; }
        public virtual IEnumerable<string> DestinationAddresses { get; set; }
        public virtual IEnumerable<Row> Rows { get; set; }
    }
}
