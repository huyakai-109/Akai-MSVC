using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Domains.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
