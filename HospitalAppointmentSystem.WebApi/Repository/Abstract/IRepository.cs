using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Repository.Abstract;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
  List<TEntity> GetAll();
  TEntity? GetById(TId id);
  TEntity? Remove(TId id);
}
