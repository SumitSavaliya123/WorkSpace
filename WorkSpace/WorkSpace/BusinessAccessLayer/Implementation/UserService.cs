using BusinessAccessLayer.Abstraction;
using DataAccessLayer.Abstraction;
using Entities.DataModels;

namespace BusinessAccessLayer.Implementation
{
    public class UserService: GenericService<User>,IUserService
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
          )
            : base(userRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors
    }
}
