-- 2021/08/21
-- Create a new table called '[User]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[User]', 'U') IS NOT NULL
DROP TABLE [dbo].[User]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[User]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Username] NVARCHAR(100) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Age] INT
    -- Specify more columns here
);
GO

ALTER TABLE User ALTER COLUMN (Id INT UNIQUE NOT NULL KEY IDENTITY(1,1)); 

-- Create a new stored procedure called 'GET_USER' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GET_USER'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.GET_USER
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.GET_USER
    @username /*parameter name*/ NVARCHAR(100) /*datatype_for_param1*/ = 'all' /*default_value_for_param1*/
-- add more stored procedure parameters here
AS
BEGIN
    -- body of the stored procedure
    IF (LOWER(@username) = 'all')
        SELECT * FROM [User] 
    ELSE
        SELECT * FROM [User] WHERE LOWER(Username) = LOWER(@username)
END
GO
-- example to executße the stored procedure we just created
EXECUTE dbo.GET_USER quocthao1998
GO

-- Create a new stored procedure called 'ADD_USER' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'ADD_USER'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.ADD_USER
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.ADD_USER
    @username NVARCHAR(100),
    @name NVARCHAR(100),
    @age int
-- add more stored procedure parameters here
AS
BEGIN
    -- body of the stored procedure
    INSERT [User] (Username, Name, Age) VALUES (@username, @name, @age)
END
GO
-- example to execute the stored procedure we just created
EXECUTE dbo.ADD_USER ABC, 'Quoc Thao', 1
GO

-- Create a new stored procedure called 'DELETE_USER' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'DELETE_USER'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.DELETE_USER
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.DELETE_USER
    @username NVARCHAR(100)
-- add more stored procedure parameters here
AS
BEGIN
    -- body of the stored procedure
    -- Delete rows from table '[User]' in schema '[dbo]'
    DELETE FROM [dbo].[User]
    WHERE LOWER(Username) = @username
END
GO
-- example to execute the stored procedure we just created
EXECUTE dbo.DELETE_USER ABC
GO

-- Create a new stored procedure called 'UPDATE_USER' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UPDATE_USER'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UPDATE_USER
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.UPDATE_USER
    @username NVARCHAR(100),
    @name NVARCHAR(100),
    @age int
-- add more stored procedure parameters here
AS
BEGIN
    -- body of the stored procedure
    -- Update rows in table '[User]' in schema '[dbo]'
    UPDATE [dbo].[User]
    SET
        [Name] = @name,
        [Age] = @age
        -- Add more columns and values here
    WHERE LOWER(Username) = @username
END
GO
-- example to execute the stored procedure we just created
EXECUTE dbo.UPDATE_USER 'quocthao1998', 'Quốc Thảo', 2
GO