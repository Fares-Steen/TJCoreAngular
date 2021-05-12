using System;

namespace TJ.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
