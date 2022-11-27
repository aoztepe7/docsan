﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.SHARED.Utils
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
