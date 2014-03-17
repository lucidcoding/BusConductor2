using System.Runtime.Serialization;

namespace BusConductor.Domain.Common
{
    /// <summary>
    /// Represents the level of a validation error.
    /// </summary>
    [DataContract]
    public enum ValidationMessageType
    {
        [EnumMember]
        Info = 0,

        [EnumMember]
        Warning = 1,

        [EnumMember]
        Error = 2
    }

}
