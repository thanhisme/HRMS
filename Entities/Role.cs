﻿namespace Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
