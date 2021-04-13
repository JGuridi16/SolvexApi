using ModelValidator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ModelValidator.ConcreteValidators
{
    public class EntityValidator<TEntity> : IEntityValidator<TEntity>
    {
        public EntityValidator()
        {
            Errors = Enumerable.Empty<string>();
        }

        public TEntity Entity { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
