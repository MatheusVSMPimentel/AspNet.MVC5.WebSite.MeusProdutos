﻿using System;

namespace MatheusVSMP.Business.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity() { 
        Id = Guid.NewGuid();
        }
    }
}
