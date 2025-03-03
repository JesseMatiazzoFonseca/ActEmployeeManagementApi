using AutoMapper;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Shared.Extensions;

namespace Domain.Interfaces.Services
{
    public class BaseService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly int _codUsuario;
        public readonly string _roles;
        public BaseService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _codUsuario = Convert.ToInt32(httpContextAccessor.CurrentUser());
            _roles = httpContextAccessor.CurrentRoles();
            _unitOfWork = unitOfWork;
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
