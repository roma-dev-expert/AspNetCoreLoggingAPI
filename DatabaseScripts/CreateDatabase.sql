CREATE TABLE LogRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Request NVARCHAR(MAX),
    Response NVARCHAR(MAX),
    Timestamp DATETIME DEFAULT GETDATE()
);

CREATE TABLE DataRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code INT NOT NULL,
    Value NVARCHAR(MAX) NOT NULL
);

