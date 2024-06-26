﻿using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces;

public interface ICarRepository
{
    Task<int> SaveCar(Car car, int dealerId);
    public Task<List<Car>> GetCarListOfUser(string userEmail);
    public Task DeleteTable();
    public Task<bool> DeleteCar(int carId);
    public Task UpdateCar(Car car);
    public Task<Car?> GetCarById(int id);
}
