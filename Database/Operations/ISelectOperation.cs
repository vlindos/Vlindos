﻿using System;
using System.Collections.Generic;
using Database.Entity;
using Database.Operations.Results;

namespace Database.Operations
{
    public interface ISelectOperation<T> : ICriteriaOperation<T, ISelectOperation<T>>
        where T : IEntity
    {
        IOperationResult<T> Retrieve(int maximum, Action<IEnumerable<EntityHolder<T>>> fetchAction);
    }
}