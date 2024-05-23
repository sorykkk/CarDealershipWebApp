using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Entities;
using Dapper;
using System.Data.SQLite;


namespace CarDealersWebApp.Data.Repositories; 

public class CarRepository : SqLiteBaseRepository, ICarRepository
{
    private static string CarTableName = ICarRepository.CarTableName;
    private static string referenceTable = IUserRepository.UserTableName;
    private static string carId = ICarRepository.CarTableId;
    private static string referenceId = IUserRepository.UserTableId;
    public CarRepository()
    {
        if (!File.Exists(DbFile))
            CreateCarTable();
    }

    public async Task<int> SaveCar(Car car, int dealerId)
    {
        using var cnn = DbConnection();
        cnn.Open();
        int carId;
        try
        {
            carId = (await cnn.QueryAsync<int>(
                $@"INSERT INTO {CarTableName}
                ({referenceId}, BrandName, Model, HP, Year, FuelType, Mileage, Price, Description, ImagePath) VALUES
                ({dealerId}, @BrandName, @Model, @HP, @Year, @FuelType, @Mileage, @Price, @Description, @ImagePath);
                select last_insert_rowid()", car)).First();
        }
        catch(SQLiteException ex)
        {
            CreateCarTable();
            carId = (await cnn.QueryAsync<int>(
                $@"INSERT INTO {CarTableName}
                ({referenceId}, BrandName, Model, HP, Year, FuelType, Mileage, Price, Description, ImagePath) VALUES
                ({dealerId}, @BrandName, @Model, @HP, @Year, @FuelType, @Mileage, @Price, @Description, @ImagePath);
                select last_insert_rowid()", car)).First();
        }

        return carId;
    }

    public async Task<List<Car>> GetCarListOfUser(string userEmail)
    {
        using var cnn = DbConnection();
        cnn.Open();
        IEnumerable<Car> cars;
        var query = $@"SELECT c.* 
                        FROM {CarTableName} c 
                        JOIN {referenceTable} r ON c.{referenceId} = r.{referenceId}
                            WHERE r.Email = @UserEmail";
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

    public async Task DeleteTable()
    {
        using var cnn = DbConnection();
        cnn.Open();

        var query = $@"DROP TABLE {CarTableName} ";
        var cars = await cnn.QueryAsync<Car>(query);
    }

    public static void CreateCarTable()
    {
        using var cnn = DbConnection();
        cnn.Open();
        cnn.Execute(
            $@"create table {CarTableName}
            (
                {carId}       INTEGER PRIMARY KEY AUTOINCREMENT,
                {referenceId} INTEGER,

                BrandName     varchar(50) not null,
                Model         varchar(50) not null,
                HP            INTEGER not null,
                Year          INTEGER not null,
                FuelType      INTEGER not null,
                Mileage       INTEGER not null,
                Price         REAL not null,
                Description   varchar(255),
                ImagePath     varchar(255) not null,

                FOREIGN  KEY({referenceId}) references {referenceTable} ({referenceId})
            );"
        );
        

    }
}

