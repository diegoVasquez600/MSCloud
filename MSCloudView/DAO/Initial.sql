CREATE DATABASE MSCloudDB
GO
USE MSCloudDB


/* Database Schema */
CREATE TABLE COUNTRY
(
    IdCountry INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CountryCode VARCHAR(10) NOT NULL,
    CountryName VARCHAR(60) NOT NULL
)

CREATE TABLE ATHLETE
(
    IdAthlete INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AthleteName VARCHAR(60) NOT NULL,
    IdCountry INT NOT NULL
)
GO
CREATE TABLE RESULTS
(
    IdResult INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    IdAthlete INT NOT NULL,
    ArranqueKG INT NOT NULL,
    EnvionKG INT NOT NULL,
    TotalPesoKG INT Not NULL
)

GO
/* Table RelationShips*/

ALTER TABLE ATHLETE
ADD FOREIGN KEY (IdCountry)
REFERENCES COUNTRY (IdCountry)
GO

ALTER TABLE RESULTS
ADD FOREIGN KEY (IdAthlete)
REFERENCES ATHLETE (IdAthlete)
GO

/* Data Insertion */

INSERT INTO COUNTRY VALUES('AUS', 'Australia'), ('CHN', 'CHINA'), ('FRA', 'FRANCIA'), ('ALE', 'ALEMANIA')
SELECT * FROM COUNTRY;
GO

INSERT INTO ATHLETE VALUES('Carlos Alviz', 1), ('Andres Sabogal', 2), ('Jorge Ortega', 3), ('Pablo Velasco', 4)
SELECT * FROM ATHLETE;
GO

INSERT INTO RESULTS VALUES(1, 134, 177, 311), (2, 130, 180, 310), (3, 125, 184, 309), (4, 0, 150, 150)
SELECT * FROM RESULTS
GO
INSERT INTO RESULTS VALUES(1, 140, 175, 308), (2, 130, 185, 307), (3, 137, 174, 306), (4, 120, 120, 140)
SELECT * FROM RESULTS
GO

/* Store Procedures */
CREATE PROC GetMainBoardData
AS
    SELECT co.CountryCode, ath.AthleteName AS AthleteName, re.ArranqueKG, re.EnvionKG, re.TotalPesoKG as TotalPesoKG FROM ATHLETE ath
    INNER JOIN COUNTRY co ON ath.IdCountry = co.IdCountry
    INNER JOIN (SELECT IdAthlete, MAX(TotalPesoKG) TotalPesoMaximo FROM RESULTS GROUP BY IdAthlete) TotalPesoMaximo ON 
    TotalPesoMaximo.IdAthlete = ath.IdAthlete
    INNER JOIN RESULTS re ON re.IdAthlete = ath.IdAthlete AND re.TotalPesoKG = TotalPesoMaximo.TotalPesoMaximo
    WHERE re.IdAthlete = ath.IdAthlete
    GROUP BY co.CountryCode, ath.AthleteName, re.ArranqueKG, re.EnvionKG, re.TotalPesoKG
    ORDER BY re.TotalPesoKG DESC
GO