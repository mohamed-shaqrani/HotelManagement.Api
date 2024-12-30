using HotelManagement.Core.Enums;

namespace HotelManagement.Service.RoleFeatureService;
public interface IRoleFeatureService
{
    Task<bool> HasAcess(Role role, Feature feature);
}
