﻿namespace DigichList.Core.Entities.Base
{
    public interface IEntityBase<TId>
    {
        public TId Id { get; set; }
    }
}
