using System;

namespace SupportAsu.Model
{
    interface IEntityMobile : IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool Deleted { get; set; }
    }
}
