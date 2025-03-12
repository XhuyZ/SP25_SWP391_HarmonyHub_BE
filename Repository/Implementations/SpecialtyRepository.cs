using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Implementations;
public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
{
}
