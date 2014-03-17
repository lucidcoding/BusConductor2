using System;

namespace Lucidity.Utilities.UnitTests.PropertyMapperTests.TestObjects
{
    public class MapTestViewModel
    {
        public string SharedString { get; set; }
        public string ViewModelOnlyString { get; set; }
        public int SharedInt { get; set; }
        public int ViewModelOnlyInt { get; set; }
        public int? SharedNullableInt { get; set; }
        public int? ViewModelOnlyNullableInt { get; set; }
        public Guid SharedGuid { get; set; }
        public Guid ViewModelOnlyGuid { get; set; }
        public Guid? SharedNullableGuid { get; set; }
        public Guid? ViewModelOnlyNullableGuid { get; set; }
        public DateTime SharedDateTime { get; set; }
        public DateTime ViewModelOnlyDateTime { get; set; }
        public DateTime? SharedNullableDateTime { get; set; }
        public DateTime? ViewModelOnlyNullableDateTime { get; set; }
        public bool SharedBool { get; set; }
        public bool ViewModelOnlyBool { get; set; }
        public bool? SharedNullableBool { get; set; }
        public bool? ViewModelOnlyNullableBool { get; set; }
        public int MismatchedProperty1 { get; set; }
        public DateTime? MismatchedProperty2 { get; set; }
        public bool MismatchedProperty3 { get; set; }
        public bool FromNullableCheckBool { get; set; }
        public int FromNullableCheckInt { get; set; }
        public DateTime FromNullableCheckDateTime { get; set; }
        public bool? ToNullableCheckBool { get; set; }
        public int? ToNullableCheckInt { get; set; }
        public DateTime? ToNullableCheckDateTime { get; set; }
    }
}
