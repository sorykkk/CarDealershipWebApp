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

        var query = $@"DROP TABLE Cars ";
        var cars = await cnn.QueryAsync<Car>(query);
    }

    public static void CreateCarTable()
    {
        using var cnn = DbConnection();
        cnn.Open();
        cnn.Execute(
            $@"create table IF NOT EXISTS Cars
            (
                ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                DealerID    INTEGER,

                BrandName     varchar(50) not null,
                Model         varchar(50) not null,
                HP            INTEGER not null,
                Year          INTEGER not null,
                FuelType      INTEGER not null,
                Mileage       INTEGER not null,
                Price        REAL not null,
                Description  varchar(255),
                ImagePath     varchar(255) not null,

                FOREIGN  KEY(DealerID) references Users (ID)
            );"
        );
        

    }
}

