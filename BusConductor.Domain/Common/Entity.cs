using System;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.Common
{
    public class Entity<T> where T : struct
    {
        protected T? _id;
        protected Guid _createdById;
        protected User _createdBy;
        protected DateTime _createdOn;
        protected Guid? _lastModifiedById;
        protected User _lastModifiedBy;
        protected DateTime? _lastModifiedOn;
        protected bool _deleted;
        protected int? _hashCode;

        public virtual T? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual Guid CreatedById
        {
            get { return _createdById; }
            set { _createdById = value; }
        }

        public virtual User CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public virtual DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        public virtual Guid? LastModifiedById
        {
            get { return _lastModifiedById; }
            set { _lastModifiedById = value; }
        }

        public virtual User LastModifiedBy
        {
            get { return _lastModifiedBy; }
            set { _lastModifiedBy = value; }
        }

        public virtual DateTime? LastModifiedOn
        {
            get { return _lastModifiedOn; }
            set { _lastModifiedOn = value; }
        }

        public virtual bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;
            if (other == null) return false;

            if (other.Id.Equals(default(T)) && _id.Equals(default(T)))
                return other == this;

            if (other.Id.Equals(default(T)) || _id.Equals(default(T)))
                return false;

            return other.Id.Equals(_id);
        }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
                return _hashCode.Value;

            if (_id.Equals(default(T)))
            {
                _hashCode = base.GetHashCode();
                return _hashCode.Value;
            }

            return _id.GetHashCode();
        }
    }
}
