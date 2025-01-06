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

    public record AddFacilityCommand(string name,int facilitycategoryid,string description) : IRequest<bool>;
    public class AddFacilityCommandHandler : IRequestHandler<AddFacilityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFacilityCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddFacilityCommand command, CancellationToken token) 
        {
            if(command.name is null  || command.facilitycategoryid <= 0 || command.name == string.Empty) 
            {
                return false;
            }

            var repo = _unitOfWork.GetRepository<Facility>();
            bool any =  await repo.AnyAsync(f=>f.FacilityName == command.name);

            if (!any) 
            {
              await  repo.AddAsync(new Facility() { FacilityName = command.name, 
                    FacilityCategoryID = command.facilitycategoryid, 
                  FacilityDescription = command.description });

                await _unitOfWork.SaveChangesAsync();

                return true;

            }

            return false;

        }
    }
}
