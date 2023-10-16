USE [Azienda]
GO
/****** Object:  Trigger [dbo].[tr_InsertRow]    Script Date: 14/03/2023 14:40:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marco Ghi
-- Create date: 07-03-2023
-- Description:	New employed update AttivitaLavortiva
-- =============================================
CREATE OR ALTER TRIGGER [dbo].[tr_InsertRow]
   ON  [dbo].[Impiegati]  
   AFTER INSERT -- procedura effettuata nella tab Impiegati, che attiva il trigger
AS 
BEGIN

  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  DECLARE @matricola nchar(4)

    -- Insert statements for trigger here
    SELECT @matricola = INSERTED.Matricola
       FROM INSERTED /* Tabella che viene passata alla Trigger, contenente i dati inseriti in Impiegati con una INSERT  */
 
       INSERT INTO AttivitaLavorativa (Matricola)
       VALUES(@matricola)
END