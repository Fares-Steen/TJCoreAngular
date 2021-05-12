using System;
namespace TJ.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
