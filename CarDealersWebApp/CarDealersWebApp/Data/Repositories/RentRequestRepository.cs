using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using Dapper;
using System.Data.SQLite;

namespace CarDealersWebApp.Data.Repositories;

public class RentRequestRepository : SqLiteBaseRepository, IRentRequestRepository
{
    public RentRequestRepository()
    {
        if (!File.Exists(DbFile))
            CreateCarTable();
    }

    public async Task<List<RentRequest>> GetRequestForDealerId(int id)
    {
        using var cnn = DbConnection();
        cnn.Open();
        IEnumerable<RentRequest> reqs;
        CreateCarTable();  // Ensure the table creation is not necessary here in production

        var query = @"SELECT r.* 
                  FROM RentRequest r
                  JOIN UserProfile u ON r.CustomerID = u.ID 
                  JOIN CarProfile c ON c.ID = r.CarID 
                  WHERE u.ID = @UserId";

        reqs = await cnn.QueryAsync<RentRequest>(query, new { UserId = id });

        return reqs.ToList();
    }

    public async Task MakeRequestDecision(int id, DecisionType decision, string? description)
    {
        using var cnn = DbConnection();
        cnn.Open();

        var query = @"UPDATE RentRequest 
                        SET Decision = @Decision,
                            Description = @Description
                            WHERE ID = @Id";

        var parameters = new { Decision = (int)decision, Id = id, Description = description};
        await cnn.ExecuteAsync(query, parameters);
    }



    private static void CreateCarTable()
    {
        using var cnn = DbConnection();
        cnn.Open();
        cnn.Execute(
            $@"CREATE TABLE IF NOT EXISTS RentRequest
            (
                ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                CarID       INTEGER NOT NULL,
                CustomerID  INTEGER NOT NULL,
                SendTime    DATETIME NOT NULL,
                FromTime    DATETIME NOT NULL,
                ToTime      DATETIME NOT NULL,
            
                Description VARCHAR(255),
                Decision    INTEGER NOT NULL,

                FOREIGN KEY(CustomerID) REFERENCES Users (ID),
                FOREIGN KEY(CarID) REFERENCES Cars (ID)
            );
        
            INSERT INTO RentRequest (CarID, CustomerID, SendTime, FromTime, ToTime, Description, Decision) 
                VALUES
                    (3, 5, '2024-05-12 10:05:00', '2024-05-22 11:00:00', '2024-05-22 16:00:00', 'I will have some pets, but I will try to be as clean as possible.', 2),
                    (4, 5, '2024-05-13 11:11:00', '2024-05-22 11:00:00', '2024-05-22 15:00:00', 'There will be also needed one car for my wife.', 2);
            "
            );
    }

}
