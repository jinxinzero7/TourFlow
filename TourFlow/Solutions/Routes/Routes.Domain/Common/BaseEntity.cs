using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Domain.Common
{
    // базовая сущность чтобы не указывать Id для всех
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
    }
}