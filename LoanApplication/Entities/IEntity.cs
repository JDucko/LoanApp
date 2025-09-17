namespace LoanApplication.Entities;

public interface IEntity<TId>
{
    TId Id { get; set; }
}
