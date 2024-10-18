﻿namespace HospitalAppointmentSystem.WebApi.Models;

public abstract class Entity<TId>
{
  protected Entity()
  {
        
  }

  protected Entity(TId id)
  {
    Id = id; 
  }

  public TId Id { get; set; }
}
