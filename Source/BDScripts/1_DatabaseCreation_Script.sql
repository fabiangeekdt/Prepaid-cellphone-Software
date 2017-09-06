-- =================================================================================
-- Author:		Fabian Andres Moreno chacon
-- Create date: Sept 02, 2017
-- Description:	Database Creation
-- =================================================================================
-- ============================= CHANGES ===========================================
-- Author:		
-- Create date: 
-- Description:	
-- =================================================================================

USE [master]
GO

CREATE DATABASE [CELLPHONE_COMPANY]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CELLPHONE_COMPANY', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CELLPHONE_COMPANY.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CELLPHONE_COMPANY_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CELLPHONE_COMPANY_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CELLPHONE_COMPANY].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ARITHABORT OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET  DISABLE_BROKER 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET  MULTI_USER 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET DB_CHAINING OFF 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [CELLPHONE_COMPANY] SET  READ_WRITE 
GO