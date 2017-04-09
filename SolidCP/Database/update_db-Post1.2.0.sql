-- This is for SQL updates after 1.2.0
--
-- Please ensure additions are added to the BOTTOM of this file
--
--

USE [${install.database}]
GO
-- update database version
DECLARE @build_version nvarchar(10), @build_date datetime
SET @build_version = N'${release.version}'
SET @build_date = '${release.date}T00:00:00' -- ISO 8601 Format (YYYY-MM-DDTHH:MM:SS)

IF NOT EXISTS (SELECT * FROM [dbo].[Versions] WHERE [DatabaseVersion] = @build_version)
BEGIN
	INSERT [dbo].[Versions] ([DatabaseVersion], [BuildDate]) VALUES (@build_version, @build_date)
END
GO

-- Fix for SP2013 wrong ProviderID

IF NOT EXISTS (SELECT * FROM [dbo].[Providers] WHERE [ProviderID] = '1552' AND [DisplayName] = 'Hosted SharePoint Enterprise 2013')
BEGIN
DECLARE @group_id AS INT
SELECT @group_id = GroupId FROM [dbo].[ResourceGroups] WHERE GroupName = 'Sharepoint Enterprise Server'
INSERT [dbo].[Providers] ([ProviderId], [GroupId], [ProviderName], [DisplayName], [ProviderType], [EditorControl], [DisableAutoDiscovery]) VALUES(1552, @group_id, N'HostedSharePoint2013Ent', N'Hosted SharePoint Enterprise 2013', N'SolidCP.Providers.HostedSolution.HostedSharePointServer2013Ent, SolidCP.Providers.HostedSolution.SharePoint2013Ent', N'HostedSharePoint30', NULL)
END
ELSE
BEGIN
UPDATE [dbo].[Providers] SET [DisableAutoDiscovery] = NULL WHERE [DisplayName] = 'Hosted SharePoint Enterprise 2013'
END
GO

IF EXISTS (SELECT * FROM [dbo].[Providers] WHERE DisplayName = 'Hosted SharePoint Enterprise 2013' AND NOT ProviderID = '1552')
BEGIN
DECLARE @SP2013provider_id INT
SET @SP2013provider_id = (SELECT ProviderID FROM [dbo].[Providers] WHERE DisplayName = 'Hosted SharePoint Enterprise 2013' AND NOT ProviderID = '1552')
UPDATE [ServiceDefaultProperties] SET [ProviderID]='1552' WHERE [ProviderID] = @SP2013provider_id
UPDATE [Services] SET [ProviderID]='1552' WHERE [ProviderID] = @SP2013provider_id
DELETE FROM [Providers] WHERE [ProviderID] = @SP2013provider_id AND DisplayName = 'Hosted SharePoint Enterprise 2013'
END
GO

-- Fix for SP2016 wrong ProviderID

IF NOT EXISTS (SELECT * FROM [dbo].[Providers] WHERE [ProviderID] = '1702' AND [DisplayName] = 'Hosted SharePoint Enterprise 2016')
BEGIN
DECLARE @group_id AS INT
SELECT @group_id = GroupId FROM [dbo].[ResourceGroups] WHERE GroupName = 'Sharepoint Enterprise Server'
INSERT [dbo].[Providers] ([ProviderId], [GroupId], [ProviderName], [DisplayName], [ProviderType], [EditorControl], [DisableAutoDiscovery]) VALUES(1702, @group_id, N'HostedSharePoint2016Ent', N'Hosted SharePoint Enterprise 2016', N'SolidCP.Providers.HostedSolution.HostedSharePointServer2016Ent, SolidCP.Providers.HostedSolution.SharePoint2016Ent', N'HostedSharePoint30', NULL)
END
ELSE
BEGIN
UPDATE [dbo].[Providers] SET [DisableAutoDiscovery] = NULL WHERE [DisplayName] = 'Hosted SharePoint Enterprise 2016'
END
GO

IF EXISTS (SELECT * FROM [dbo].[Providers] WHERE DisplayName = 'Hosted SharePoint Enterprise 2016' AND NOT ProviderID = '1702')
BEGIN
DECLARE @SP2016provider_id INT
SET @SP2016provider_id = (SELECT ProviderID FROM [dbo].[Providers] WHERE DisplayName = 'Hosted SharePoint Enterprise 2016' AND NOT ProviderID = '1702')
UPDATE [ServiceDefaultProperties] SET [ProviderID]='1702' WHERE [ProviderID] = @SP2016provider_id
UPDATE [Services] SET [ProviderID]='1702' WHERE [ProviderID] = @SP2016provider_id
DELETE FROM [Providers] WHERE [ProviderID] = @SP2016provider_id AND DisplayName = 'Hosted SharePoint Enterprise 2016'
END
GO

-- SimpleDNS 6.x
IF NOT EXISTS (SELECT * FROM [dbo].[Providers] WHERE [ProviderID] = '1703' AND DisplayName = 'SimpleDNS Plus 6.x')
BEGIN
INSERT [dbo].[Providers] ([ProviderID], [GroupID], [ProviderName], [DisplayName], [ProviderType], [EditorControl], [DisableAutoDiscovery]) VALUES (1703, 7, N'SimpleDNS', N'SimpleDNS Plus 6.x', N'SolidCP.Providers.DNS.SimpleDNS6, SolidCP.Providers.DNS.SimpleDNS60', N'SimpleDNS', NULL)
END
GO