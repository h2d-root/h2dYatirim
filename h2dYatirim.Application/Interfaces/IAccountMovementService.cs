using Core.Utilities.Results;
using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface IAccountMovementService
    {
        IDataResult<List<AccountMovement>> GetAccountMovement(Guid userId);

    }
}
