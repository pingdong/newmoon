IF NOT EXISTS ( SELECT  *
				FROM    sys.schemas
				WHERE   name = N'Events' ) 
	EXEC('CREATE SCHEMA Events AUTHORIZATION dbo');
GO