SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Events].[Attendees](
	[AttendeeId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[AttendeeIdentity] [uniqueidentifier] NOT NULL,
	[AttendeeFirstName] [nvarchar](200) NOT NULL,
	[AttendeeLastName] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Events].[Attendees]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [Events].[Events] ([EventId])
GO