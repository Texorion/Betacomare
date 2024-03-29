USE [Azienda]
GO
/****** Object:  Trigger [dbo].[tr_DeleteRow]    Script Date: 14/03/2023 14:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marco Ghi
-- Create date: 13-03-2023
-- Description:	Delete employed update AttivitaLavortiva
-- =============================================
CREATE OR ALTER TRIGGER [dbo].[tr_DeleteRow]
   ON  [dbo].[Impiegati]
   AFTER DELETE -- procedura effettuata nella tab Impiegati, che attiva il trigger
AS 
BEGIN
  SET NOCOUNT ON;
  DECLARE @matricola nchar(4)

    -- Insert statements for trigger here
    SELECT @matricola = DELETED.Matricola
       FROM DELETED /* Tabella che viene passata alla Trigger, contenente i dati cancellati da Impiegati con una DELETE  */

	   -- PRINT @matricola;
 
       DELETE FROM AttivitaLavorativa
       WHERE Matricola = @matricola
END