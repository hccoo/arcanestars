﻿namespace ArcaneStars.Service.Domain
{
    public abstract class Entity<T>: IEntity
    {
        T _id;

        public virtual T Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
    }
}
