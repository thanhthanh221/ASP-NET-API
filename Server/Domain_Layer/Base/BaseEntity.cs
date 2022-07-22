using System;

namespace Domain_Layer.Base
{
    public abstract class BaseEntity
    {
        public Guid Id {get; set;}

        public override bool Equals(object obj)
        {
            BaseEntity baseEntity = obj as BaseEntity;
            if(baseEntity is null)
            {
                return false;
            }
            return baseEntity.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}