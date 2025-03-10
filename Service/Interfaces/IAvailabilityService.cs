using Domain.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAvailabilityService
    {
        Task UpdateAvailability(int id, UpdateAvailabilityRequest request);
    }
}
