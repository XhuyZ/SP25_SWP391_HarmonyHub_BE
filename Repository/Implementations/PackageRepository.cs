using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Implementations;

public class PackageRepository : GenericRepository<Package>, IPackageRepository;