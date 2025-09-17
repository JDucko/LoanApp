namespace LoanApplication.Models;

public interface IEntity<TId>
{
    TId Id { get; set; }
}
