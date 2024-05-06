using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface IAccountMovementService
    {
        public List<AccountMovement> GetAccountMovement(Guid id);
    }
}
