﻿using System.Collections.Generic;
using System.Linq;

using FourWheels.Data.Models;
using FourWheels.Data.Repositories;
using FourWheels.Services.Contracts;

using Bytes2you.Validation;

namespace FourWheels.Services
{
    public class TownServices : ITownServices
    {
        private readonly IEfRepostory<Town> townsRepo;

        public TownServices(IEfRepostory<Town> townsRepo)
        {
            Guard.WhenArgument(townsRepo, "townsRepo").IsNull().Throw();

            this.townsRepo = townsRepo;
        }

        public IEnumerable<Town> GetAllTowns()
        {
            return this.townsRepo.All.ToList();
        }
    }
}
