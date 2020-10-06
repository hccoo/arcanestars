using ArcaneStars.Infrastructure;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Infrastructure.Exceptions;
using ArcaneStars.UserService.Applications.Dtos;
using ArcaneStars.UserService.Domains.Aggregates;
using ArcaneStars.UserService.Domains.Repositories;
using AutoMapper;
using System;
using System.Linq;

namespace ArcaneStars.UserService.Applications.Services
{
    public interface IUserAppService : IApplicationServiceContract
    {
        void AddUser(UserDto model, string operatedBy);

        void SuspendUser(string userName);

        UserDto Get(string userName);

        UserDto CheckUser(string userName, string password);
    }

    public class UserAppService : ApplicationServiceContract, IUserAppService
    {
        readonly IEventBus _eventBus;
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;

        public UserAppService(
            IMapper mapper,
            IUserRepository userRepository,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus) : base(dbUnitOfWork)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserDto model, string operatedBy)
        {
            if (string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.Mobile)) throw new AppServiceException("User name and mobile can not be null.");

            var user = _userRepository.GetFiltered(o => o.UserName == model.UserName).FirstOrDefault();

            if (user != null) throw new AppServiceException("User name already exists in the db.");

            user = UserFactory.CreateInstance(model.UserName, model.Mobile, model.Email, model.Password, model.NickName, operatedBy);
            _userRepository.Add(user);
            _dbUnitOfWork.Commit();
        }

        public UserDto CheckUser(string userName, string password)
        {
            var user = _userRepository.GetFiltered(o => o.UserName == userName).FirstOrDefault();
            if (user == null) return null;

            if (!user.CheckPassword(password)) return null;

            user.HidePassword();
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public UserDto Get(string userName)
        {
            var user = _userRepository.GetFiltered(o => o.UserName == userName).FirstOrDefault();
            if (user == null) return null;
            user.HidePassword();
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public void SuspendUser(string userName)
        {
            var user = _userRepository.GetFiltered(o => o.UserName == userName).FirstOrDefault();
            user.SetSuspend();
            _userRepository.Update(user);
            _dbUnitOfWork.Commit();
        }
    }
}
