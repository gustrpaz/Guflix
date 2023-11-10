USE GUFLIX
GO

INSERT INTO Usuario(UserName,Email,Passwd)
VALUES ('Gustavo','gustavo@email.com','gu@123'),
('Livia','livia@email.com','li@123'),
('Carlos','carlos@email.com','ca@123');
GO

INSERT INTO Filme(NomeFilme,Genero)
VALUES('After 2','Romance'),('Gato de Botas','Aventura'),('Até o último homem','Ação');
GO

SELECT * FROM Usuario
SELECT * FROM Filme