USE [master]
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ClickOnceFileRepository')
BEGIN
--change the file names to the location where you want the database to be created
CREATE DATABASE [ClickOnceFileRepository] ON  PRIMARY 
( NAME = N'ClickOnceFileRepository', FILENAME = 
N'E:\SQLServerData\Data\ClickOnceFileRepository.mdf' , 
SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ClickOnceFileRepository_log', FILENAME = 
N'E:\SQLServerData\Data\ClickOnceFileRepository_log.LDF' , 
SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END
