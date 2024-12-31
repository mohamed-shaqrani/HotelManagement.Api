using HotelManagement.Core.Enums;

namespace HotelManagement.Core.Entities.UserManagement;
public class RoleFeature : BaseEntity
{
    public Role Role { get; set; }
    public Feature Feature { get; set; }
}

