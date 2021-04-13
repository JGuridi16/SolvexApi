using System.Collections.Generic;

namespace ModelValidator.Interfaces
{
    public interface IEntityValidator<T>
    {
        public T Entity { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
