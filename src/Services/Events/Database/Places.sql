SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Events].[Places](
	[PlaceId] [int] NOT NULL,
	[PlaceName] [nvarchar](200) NOT NULL,
	[IsOccupied] [bit] NOT NULL,
	[AddressNo] [nvarchar](20) NOT NULL,
	[AddressStreet] [nvarchar](100) NOT NULL,
	[AddressCity] [nvarchar](40) NOT NULL,
	[AddressState] [nvarchar](40) NOT NULL,
	[AddressCountry] [nvarchar](40) NOT NULL,
	[AddressZipCode] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE SEQUENCE [Events].PlaceSeq 
	AS [bigint]
	START WITH 1
		INCREMENT BY 10
		MINVALUE -9223372036854775808
		MAXVALUE 9223372036854775807
		CACHE 
GO