IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [NomineeDtos] (
        [Id] uniqueidentifier NOT NULL,
        [Fullname] nvarchar(max) NULL,
        [ImageUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_NomineeDtos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [PollDtos] (
        [Id] uniqueidentifier NOT NULL,
        [Theme] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [StartedDateTime] datetime2 NOT NULL,
        [EndedDateTime] datetime2 NOT NULL,
        CONSTRAINT [PK_PollDtos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [ActivityDtos] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NULL,
        [Action] nvarchar(max) NULL,
        CONSTRAINT [PK_ActivityDtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ActivityDtos_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [RefreshTokens] (
        [Token] nvarchar(450) NOT NULL,
        [JwtId] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [Used] bit NOT NULL,
        [Invalidated] bit NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Token]),
        CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [CategoryDtos] (
        [Id] uniqueidentifier NOT NULL,
        [Theme] nvarchar(max) NULL,
        [PollDtoId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CategoryDtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CategoryDtos_PollDtos_PollDtoId] FOREIGN KEY ([PollDtoId]) REFERENCES [PollDtos] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [ElectoralCommisionerDtos] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NULL,
        [PollDtoId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ElectoralCommisionerDtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ElectoralCommisionerDtos_PollDtos_PollDtoId] FOREIGN KEY ([PollDtoId]) REFERENCES [PollDtos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ElectoralCommisionerDtos_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [CategoryNomineeDtos] (
        [Id] uniqueidentifier NOT NULL,
        [CategoryDtoId] uniqueidentifier NOT NULL,
        [NomineeDtoId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CategoryNomineeDtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CategoryNomineeDtos_CategoryDtos_CategoryDtoId] FOREIGN KEY ([CategoryDtoId]) REFERENCES [CategoryDtos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CategoryNomineeDtos_NomineeDtos_NomineeDtoId] FOREIGN KEY ([NomineeDtoId]) REFERENCES [NomineeDtos] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE TABLE [VoteDtos] (
        [Id] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UserId] nvarchar(450) NULL,
        [NomineeDtoId] uniqueidentifier NOT NULL,
        [CategoryDtoId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_VoteDtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VoteDtos_CategoryDtos_CategoryDtoId] FOREIGN KEY ([CategoryDtoId]) REFERENCES [CategoryDtos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_VoteDtos_NomineeDtos_NomineeDtoId] FOREIGN KEY ([NomineeDtoId]) REFERENCES [NomineeDtos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_VoteDtos_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_ActivityDtos_UserId] ON [ActivityDtos] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_CategoryDtos_PollDtoId] ON [CategoryDtos] ([PollDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_CategoryNomineeDtos_CategoryDtoId] ON [CategoryNomineeDtos] ([CategoryDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_CategoryNomineeDtos_NomineeDtoId] ON [CategoryNomineeDtos] ([NomineeDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_ElectoralCommisionerDtos_PollDtoId] ON [ElectoralCommisionerDtos] ([PollDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_ElectoralCommisionerDtos_UserId] ON [ElectoralCommisionerDtos] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_VoteDtos_CategoryDtoId] ON [VoteDtos] ([CategoryDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_VoteDtos_NomineeDtoId] ON [VoteDtos] ([NomineeDtoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    CREATE INDEX [IX_VoteDtos_UserId] ON [VoteDtos] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200711190600_First Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200711190600_First Migration', N'3.1.2');
END;

GO

