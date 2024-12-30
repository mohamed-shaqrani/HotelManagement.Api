using HotelManagement.Core.Entities;
using HotelManagement.Core.Enums;

namespace HotelManagement.Core.Entities;
public class RoleFeature : BaseEntity
{
    public Role Role { get; set; }
    public Feature Feature { get; set; }
}

