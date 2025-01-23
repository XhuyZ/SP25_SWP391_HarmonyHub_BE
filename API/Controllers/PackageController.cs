using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class PackageController: ApiBaseController
{
    private readonly IPackageService _packageService;

    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }
}