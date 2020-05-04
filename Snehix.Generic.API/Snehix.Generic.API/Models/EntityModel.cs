﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snehix.Generic.API.Models
{
    public class EntityModel: EntityTypeModel
    {
        public int EntityTypeId { get; set; }
    }

    public class EntityTypeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
