--
-- File generated with SQLiteStudio v3.2.1 on Вт окт 27 16:00:32 2020
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Cities
DROP TABLE IF EXISTS Cities;
CREATE TABLE Cities (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);
INSERT INTO Cities (Id, Name) VALUES (1, 'Москва');
INSERT INTO Cities (Id, Name) VALUES (2, 'Санкт-Петербург');
INSERT INTO Cities (Id, Name) VALUES (3, 'Архангельск');

-- Table: Restaurants
DROP TABLE IF EXISTS Restaurants;
CREATE TABLE Restaurants (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, CityId INTEGER REFERENCES Cities (Id) NOT NULL);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (1, '800C Contemporary Steak', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (2, 'Andiamo на Ленинском', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (3, 'Белладжио', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (4, 'Bizone Hookah в Жулебино', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (5, 'Bruce Lee / Брюс Ли', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (6, 'Burgers And Crabs', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (7, 'Cantinetta Antinori', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (8, 'Catavina / Катавина', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (9, 'Columbus Lounge', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (10, 'Corner Cafe&Kitchen', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (11, 'Хороший Год', 1);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (12, 'Хуан-Ди / Huandy', 2);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (13, 'Хуторок', 2);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (14, 'Чабан Хаус', 2);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (15, 'Три Мудреца', 2);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (16, 'Тютчевъ', 2);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (17, 'У Трех Сестер', 3);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (18, 'Сэр Ежик', 3);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (19, 'Тадж Махал Lounge', 3);
INSERT INTO Restaurants (Id, Name, CityId) VALUES (20, 'Тайгер', 3);

-- Index: Name_CityId_INDX
DROP INDEX IF EXISTS Name_CityId_INDX;
CREATE UNIQUE INDEX Name_CityId_INDX ON Restaurants (Name, CityId);

-- Index: Name_INDX
DROP INDEX IF EXISTS Name_INDX;
CREATE UNIQUE INDEX Name_INDX ON Cities (Name);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
