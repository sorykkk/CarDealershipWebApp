using CarDealersWebApp.Data.Interfaces;
using Dapper;


namespace CarDealersWebApp.Data.Repositories; 

public class CarRepository : SqLiteBaseRepository, ICarRepository
{
    public static string CarTableName { get; set; } = "CarProfile";
    private static string referenceTable;
    private static string referenceId;
    public CarRepository(string referenceTableName, string referenceTableId)
    {
        referenceTable = referenceTableName;
        referenceId = referenceTableId;
        if (!File.Exists(DbFile))
            CreateCarTable();
    }

    public static void CreateCarTable()
    {
        using var cnn = DbConnection();
        cnn.Open();
        cnn.Execute(
            $@"create table {CarTableName}
            (
                CAR_ID     INTEGER PRIMARY KEY AUTOINCREMENT,
                foreign key(DEALER_ID) references {referenceTable} ({referenceId})
            )"
            );
    }
}

