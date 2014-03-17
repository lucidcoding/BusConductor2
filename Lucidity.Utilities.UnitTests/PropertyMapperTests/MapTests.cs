using System;
using Lucidity.Utilities.UnitTests.PropertyMapperTests.TestObjects;
using NUnit.Framework;

namespace Lucidity.Utilities.UnitTests.PropertyMapperTests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void PopulatedMatchingPropertiesAreMappedAndOthersAreIgnored()
        {
            var entity = new MapTestEntity
                             {
                                 SharedString = "test string 1",
                                 EntityOnlyString = "test string 2",
                                 SharedInt = 36,
                                 EntityOnlyInt = 87,
                                 SharedNullableInt = 71,
                                 EntityOnlyNullableInt = 198,
                                 SharedGuid = Guid.NewGuid(),
                                 EntityOnlyGuid = Guid.NewGuid(),
                                 SharedNullableGuid = Guid.NewGuid(),
                                 EntityOnlyNullableGuid = Guid.NewGuid(),
                                 SharedDateTime = new DateTime(2000, 10, 5, 9, 30, 01),
                                 EntityOnlyDateTime = new DateTime(2000, 10, 5, 9, 30, 02),
                                 SharedNullableDateTime = new DateTime(2000, 10, 5, 9, 30, 03),
                                 EntityOnlyNullableDateTime = new DateTime(2000, 10, 5, 9, 30, 04),
                                 SharedBool = true,
                                 EntityOnlyBool = true,
                                 SharedNullableBool = true,
                                 EntityOnlyNullableBool = true,
                                 MismatchedProperty1 = "test string 3",
                                 MismatchedProperty2 = new DateTime(2000, 10, 5, 9, 30, 05),
                                 MismatchedProperty3 = true,
                                 FromNullableCheckBool = true,
                                 FromNullableCheckInt = 92,
                                 FromNullableCheckDateTime = new DateTime(2000, 10, 5, 9, 30, 06),
                                 ToNullableCheckBool = true,
                                 ToNullableCheckInt = 18,
                                 ToNullableCheckDateTime = new DateTime(2000, 10, 5, 9, 30, 07),
                             };

            var viewModel = PropertyMapper.MapMatchingProperties<MapTestEntity, MapTestViewModel>(entity);
            Assert.That(viewModel.SharedString, Is.EqualTo(entity.SharedString));
            Assert.That(viewModel.SharedInt, Is.EqualTo(entity.SharedInt));
            Assert.That(viewModel.SharedNullableInt, Is.EqualTo(entity.SharedNullableInt));
            Assert.That(viewModel.SharedGuid, Is.EqualTo(entity.SharedGuid));
            Assert.That(viewModel.SharedNullableGuid, Is.EqualTo(entity.SharedNullableGuid));
            Assert.That(viewModel.SharedDateTime, Is.EqualTo(entity.SharedDateTime));
            Assert.That(viewModel.SharedNullableDateTime, Is.EqualTo(entity.SharedNullableDateTime));
            Assert.That(viewModel.SharedBool, Is.EqualTo(entity.SharedBool));
            Assert.That(viewModel.SharedNullableBool, Is.EqualTo(entity.SharedNullableBool));
            Assert.That(viewModel.ViewModelOnlyString, Is.Null);
            Assert.That(viewModel.ViewModelOnlyInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.ViewModelOnlyNullableInt, Is.Null);
            Assert.That(viewModel.ViewModelOnlyGuid, Is.EqualTo(Guid.Empty));
            Assert.That(viewModel.ViewModelOnlyNullableGuid, Is.Null);
            Assert.That(viewModel.ViewModelOnlyDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.ViewModelOnlyNullableDateTime, Is.Null);
            Assert.That(viewModel.ViewModelOnlyBool, Is.False);
            Assert.That(viewModel.ViewModelOnlyNullableBool, Is.Null);
            Assert.That(viewModel.MismatchedProperty1, Is.EqualTo(default(int)));
            Assert.That(viewModel.MismatchedProperty2, Is.Null);
            Assert.That(viewModel.MismatchedProperty3, Is.False);
            Assert.That(viewModel.FromNullableCheckBool, Is.False);
            Assert.That(viewModel.FromNullableCheckInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.FromNullableCheckDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.ToNullableCheckBool, Is.Null);
            Assert.That(viewModel.ToNullableCheckInt, Is.Null);
            Assert.That(viewModel.ToNullableCheckDateTime, Is.Null);
        }

        [Test]
        public void NullPropertiesRemainNull()
        {
            var entity = new MapTestEntity();
            var viewModel = PropertyMapper.MapMatchingProperties<MapTestEntity, MapTestViewModel>(entity);
            Assert.That(viewModel.SharedString, Is.Null);
            Assert.That(viewModel.SharedInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.SharedNullableInt, Is.Null);
            Assert.That(viewModel.SharedGuid, Is.EqualTo(Guid.Empty));
            Assert.That(viewModel.SharedNullableGuid, Is.Null);
            Assert.That(viewModel.SharedDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.SharedNullableDateTime, Is.Null);
            Assert.That(viewModel.SharedBool, Is.False);
            Assert.That(viewModel.SharedNullableBool, Is.Null);
            Assert.That(viewModel.ViewModelOnlyString, Is.Null);
            Assert.That(viewModel.ViewModelOnlyInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.ViewModelOnlyNullableInt, Is.Null);
            Assert.That(viewModel.ViewModelOnlyGuid, Is.EqualTo(Guid.Empty));
            Assert.That(viewModel.ViewModelOnlyNullableGuid, Is.Null);
            Assert.That(viewModel.ViewModelOnlyDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.ViewModelOnlyNullableDateTime, Is.Null);
            Assert.That(viewModel.ViewModelOnlyBool, Is.False);
            Assert.That(viewModel.ViewModelOnlyNullableBool, Is.Null);
            Assert.That(viewModel.MismatchedProperty1, Is.EqualTo(default(int)));
            Assert.That(viewModel.MismatchedProperty2, Is.Null);
            Assert.That(viewModel.MismatchedProperty3, Is.False);
            Assert.That(viewModel.FromNullableCheckBool, Is.False);
            Assert.That(viewModel.FromNullableCheckInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.FromNullableCheckDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.ToNullableCheckBool, Is.Null);
            Assert.That(viewModel.ToNullableCheckInt, Is.Null);
            Assert.That(viewModel.ToNullableCheckDateTime, Is.Null);
        }

        [Test]
        public void NullablesAreMappedToNonNullablesAndViceVersa()
        {
            var entity = new MapTestEntity
            {
                FromNullableCheckBool = true,
                FromNullableCheckInt = 92,
                FromNullableCheckDateTime = new DateTime(2000, 10, 5, 9, 30, 06),
                ToNullableCheckBool = true,
                ToNullableCheckInt = 18,
                ToNullableCheckDateTime = new DateTime(2000, 10, 5, 9, 30, 07),
            };

            var viewModel = PropertyMapper.MapMatchingProperties<MapTestEntity, MapTestViewModel>(entity, true);
            Assert.That(viewModel.FromNullableCheckBool, Is.True);
            Assert.That(viewModel.FromNullableCheckInt, Is.EqualTo(entity.FromNullableCheckInt));
            Assert.That(viewModel.FromNullableCheckDateTime, Is.EqualTo(entity.FromNullableCheckDateTime));
            Assert.That(viewModel.ToNullableCheckBool, Is.True);
            Assert.That(viewModel.ToNullableCheckInt, Is.EqualTo(entity.ToNullableCheckInt));
            Assert.That(viewModel.ToNullableCheckDateTime, Is.EqualTo(entity.ToNullableCheckDateTime));
        }

        [Test]
        public void NullablesWithoutValueAreMappedToDefaultAndViceVersa()
        {
            var entity = new MapTestEntity();
            var viewModel = PropertyMapper.MapMatchingProperties<MapTestEntity, MapTestViewModel>(entity, true);
            Assert.That(viewModel.FromNullableCheckBool, Is.False);
            Assert.That(viewModel.FromNullableCheckInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.FromNullableCheckDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(viewModel.ToNullableCheckBool, Is.False);
            Assert.That(viewModel.ToNullableCheckInt, Is.EqualTo(default(int)));
            Assert.That(viewModel.ToNullableCheckDateTime, Is.EqualTo(default(DateTime)));
        }
    }
}
