﻿using AutoMapper;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public UserProfileViewModel Login(LoginCredentialsViewModel loginInfo)
        {
            _accountRepository.UnitOfWork = _unitOfWork;

            return Mapper.Map<UserProfileViewModel>(_accountRepository.Login(loginInfo.Email, loginInfo.Password));

            //_unitOfWork.Begin();
            //try
            //{
            //    _accountRepository.Login(loginInfo.UserName, loginInfo.Password);

            //    _unitOfWork.Commit();
            //}
            //catch(Exception ex)
            //{
            //    _unitOfWork.Rollback();
            //    throw;
            //}

            //return false;
        }
    }
}