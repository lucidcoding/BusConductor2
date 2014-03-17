using System;

namespace Lucidity.Utilities.UnitTests.PropertyMapperTests.TestObjects
{
    public class MapTestEntity
    {
        public string SharedString { get; set; }
        public string EntityOnlyString { get; set; }
        public int SharedInt { get; set; }
        public int EntityOnlyInt { get; set; }
        public int? SharedNullableInt { get; set; }
        public int? EntityOnlyNullableInt { get; set; }
        public Guid SharedGuid { get; set; }
        public Guid EntityOnlyGuid { get; set; }
        public Guid? SharedNullableGuid { get; set; }
        public Guid? EntityOnlyNullableGuid { get; set; }
        public DateTime SharedDateTime { get; set; }
        public DateTime EntityOnlyDateTime { get; set; }
        public DateTime? SharedNullableDateTime { get; set; }
        public DateTime? EntityOnlyNullableDateTime { get; set; }
        public bool SharedBool { get; set; }
        public bool EntityOnlyBool { get; set; }
        public bool? SharedNullableBool { get; set; }
        public bool? EntityOnlyNullableBool { get; set; }
        public string MismatchedProperty1 { get; set; }
        public DateTime MismatchedProperty2 { get; set; }
        public bool? MismatchedProperty3 { get; set; }
        public bool? FromNullableCheckBool { get; set; }
        public int? FromNullableCheckInt { get; set; }
        public DateTime? FromNullableCheckDateTime { get; set; }
        public bool ToNullableCheckBool { get; set; }
        public int ToNullableCheckInt { get; set; }
        public DateTime ToNullableCheckDateTime { get; set; }
    }
}
