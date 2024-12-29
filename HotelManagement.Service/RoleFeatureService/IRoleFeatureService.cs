using HotelManagement.App.Core.Enums;

namespace HotelManagement.App.Service.RoleFeatureService;
public interface IRoleFeatureService
{
    Task<bool> HasAcess(Role role, Feature feature);
}
