
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/10/2018 09:50:45
-- Generated from EDMX file: C:\Users\John\Source\Repos\Northwind\Northwind_John\Northwind_John\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dept_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[userSet] DROP CONSTRAINT [FK_dept_user];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[userSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[userSet];
GO
IF OBJECT_ID(N'[dbo].[deptSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[deptSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'userSet'
CREATE TABLE [dbo].[userSet] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [user_name] nvarchar(max)  NOT NULL,
    [dept_id] int  NOT NULL,
    [update_date] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'deptSet'
CREATE TABLE [dbo].[deptSet] (
    [dept_id] int IDENTITY(1,1) NOT NULL,
    [dept_name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [user_id] in table 'userSet'
ALTER TABLE [dbo].[userSet]
ADD CONSTRAINT [PK_userSet]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [dept_id] in table 'deptSet'
ALTER TABLE [dbo].[deptSet]
ADD CONSTRAINT [PK_deptSet]
    PRIMARY KEY CLUSTERED ([dept_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [dept_id] in table 'userSet'
ALTER TABLE [dbo].[userSet]
ADD CONSTRAINT [FK_dept_user]
    FOREIGN KEY ([dept_id])
    REFERENCES [dbo].[deptSet]
        ([dept_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dept_user'
CREATE INDEX [IX_FK_dept_user]
ON [dbo].[userSet]
    ([dept_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------