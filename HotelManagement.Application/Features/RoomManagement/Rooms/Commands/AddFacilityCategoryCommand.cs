using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Commands
{
    public record AddFacilityCategoryCommand(string name) : IRequest<bool>;
    public class AddFacilityCategoryCommandHandler: IRequestHandler<AddFacilityCategoryCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public async Task<bool> Handle(AddFacilityCategoryCommand command,CancellationToken cancellation) 
        {
            var repo = _unitOfWork.GetRepository<FacilityCategory>();
            bool any = false;
            if(command.name is not null) 
            {  
                any = await repo.AnyAsync(f => f.FacilityCategoryName == command.name);

            }
            else 
            {
                return false;
            }
          
            if (!any ) 
            {
             await repo.AddAsync(new FacilityCategory() { FacilityCategoryName = command.name });
                return true;
            }

            return false;
        }
    }
}
