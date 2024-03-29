﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Abstract
{
    public class TimestampedEntity<T> : IdentityEntity<T>
    {
        [Column("created_on")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        [Column("updated_on")]
        public DateTimeOffset? UpdatedOn { get;set; } = DateTimeOffset.UtcNow;
    }
}
