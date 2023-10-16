USE [Azienda]
GO

/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 10/03/2023 10:24:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alessandro Ianniello
-- Create date: 2023-03-10
-- Description:	Insert Impiegato
-- =============================================
CREATE OR ALTER PROCEDURE sp_InsertImpiegato
	@matricola nchar(4) = NULL,
	@nome nvarchar(50) = NULL,
	@cognome nvarchar(50) = NULL,
	@dataAssunzione date = NULL,
	@indirizzo nvarchar(50) = NULL,
	@CAP nvarchar(10) = NULL,
	@citta nvarchar(30) = NULL,
	@provincia nchar(2) = NULL,
	@telefono nvarchar(15) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Anno INT = 1900
	DECLARE @Mese INT = 01
	DECLARE @Giorno INT = 01

	-- @Anno = YEAR(@dataAssunzione) AND @Mese = MONTH(@dataAssunzione) AND @Giorno = DAY(@dataAssunzione)
	IF(@dataAssunzione LIKE '01/01/1900%')
	BEGIN
		SET @dataAssunzione = NULL
	END

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Impiegati]
           ([Matricola]           
           ,[Nome]
           ,[Cognome]
		   ,[DataAssunzione]
           ,[Indirizzo]
           ,[CAP]
		   ,[Citta]
           ,[Provincia]
           ,[Telefono])

     VALUES (@matricola
			,@nome
			,@cognome
			,@dataAssunzione
			,@indirizzo
			,@CAP
			,@citta
			,@provincia
			,@telefono)
END
GO
