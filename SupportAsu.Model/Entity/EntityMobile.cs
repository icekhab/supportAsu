using System;
using System.ComponentModel;

namespace SupportAsu.Model
{
    public abstract class EntityMobile : IEntityMobile
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
