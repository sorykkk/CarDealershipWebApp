using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Entities;
using Dapper;
using System.Data.SQLite;


namespace CarDealersWebApp.Data.Repositories; 

public class CarRepository : SqLiteBaseRepository, ICarRepository
{
    public CarRepository()
    {
        if (!File.Exists(DbFile))
            CreateCarTable();
    }

    public async Task<int> SaveCar(Car car, int dealerId)
    {
        using var cnn = DbConnection();
        cnn.Open();
        CreateCarTable();
        var carId = (await cnn.QueryAsync<int>(
            $@"INSERT INTO Cars
            (DealerID, BrandName, Model, HP, Year, FuelType, Mileage, Price, Description, ImagePath) VALUES
            ({dealerId}, @BrandName, @Model, @HP, @Year, @FuelType, @Mileage, @Price, @Description, @ImagePath);
            select last_insert_rowid()", car)).First();

        return carId;
    }

    public async Task UpdateCar(Car car)
    {
        using var cnn = DbConnection();
        cnn.Open();

        var query = @"UPDATE Cars
                        SET BrandName = @BrandName, 
                            Model = @Model, 
                            HP = @HP, 
                            Year = @Year, 
                            FuelType = @FuelType, 
                            Mileage = @Mileage, 
                            Price = @Price, 
                            Description = @Description, 
                            ImagePath = @ImagePath
                        WHERE Id = @Id";

        var parameters = new
        {
            car.BrandName,
            car.Model,
            car.HP,
            car.Year,
            car.FuelType,
            car.Mileage,
            car.Price,
            car.Description,
            car.ImagePath,
            car.Id
        };

        await cnn.ExecuteAsync(query, parameters);
    }

    public async Task<List<Car>> GetCarListOfUser(string userEmail)
    {
        using var cnn = DbConnection();
        cnn.Open();
        IEnumerable<Car> cars;
        var query = $@"SELECT c.* 
                        FROM Cars c 
                        JOIN Users u ON c.DealerID = u.ID
                            WHERE u.Email = @UserEmail";
        try
        {
            cars = await cnn.QueryAsync<Car>(query, new {UserEmail = userEmail}) ;
        }
        catch(SQLiteException e)
        {
            CreateCarTable();
            cars = await cnn.QueryAsync<Car>(query, new { UserEmail = userEmail });
        }

        return cars.ToList();
    }

    public async Task<Car?> GetCarById(int id)
    {
        using var cnn = DbConnection();
        cnn.Open();
        var query = @"SELECT * FROM Cars WHERE Id = @Id";
        var car = await cnn.QuerySingleOrDefaultAsync<Car>(query, new {Id = id});

        return car;
    }

    public async Task<bool> DeleteCar(int carId)
    {
        using var cnn = DbConnection();
        cnn.Open();
        var query = $@"DELETE FROM Cars WHERE ID = @CarId";

        var rowsAffected = await cnn.ExecuteAsync(query, new {CarId = carId});

        return rowsAffected > 0;
    }

    public async Task DeleteTable()
    {
        using var cnn = DbConnection();
        cnn.Open();

        var query = @"DROP TABLE IF EXISTS Cars";
        await cnn.ExecuteAsync(query);
    }


    private static void CreateCarTable()
    {
        using var cnn = DbConnection();
        cnn.Open();
        cnn.Execute(
            $@"create table IF NOT EXISTS Cars
            (
                ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                DealerID    INTEGER,

                BrandName   varchar(50) not null,
                Model       varchar(50) not null,
                HP          INTEGER not null,
                Year        INTEGER not null,
                FuelType    INTEGER not null,
                Mileage     INTEGER not null,
                Price       REAL not null,
                Description varchar(255),
                ImagePath   varchar(255) not null,

                FOREIGN  KEY(DealerID) references Users (ID)
            );"
        );
    }
}

