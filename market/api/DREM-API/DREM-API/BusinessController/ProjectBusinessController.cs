﻿using DREM_API.Model;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    public class ProjectBusinessController
    {
        private readonly ProjectRepository repository;
        public ProjectBusinessController(ProjectRepository repository)
        {
            this.repository = repository;
        }

    }
}
