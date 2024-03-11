
go

use OUCR20241103DB

go


-- Create the table in the specified schema
CREATE TABLE Proveedor
(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    Nombre [NVARCHAR](50) NOT NULL,
    Dui [NVARCHAR](50) NOT NULL,
);

GO
-- Create the table in the specified schema
CREATE TABLE Direcciones
(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    Calle [NVARCHAR](50) NOT NULL,
    NumeoDeCasa [NVARCHAR](50) NOT NULL,
    IdProveedor int,
   FOREIGN KEY (IdProveedor) REFERENCES Proveedor(Id) ON DELETE CASCADE
);
GO