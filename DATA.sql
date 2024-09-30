/*
 Navicat Premium Data Transfer

 Source Server         : SQL Server
 Source Server Type    : SQL Server
 Source Server Version : 15002000 (15.00.2000)
 Source Host           : Melodia\SQLEXPRESS01:1433
 Source Catalog        : biblioteca
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000 (15.00.2000)
 File Encoding         : 65001

 Date: 05/11/2023 13:39:01
*/


-- ----------------------------
-- Table structure for autores
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[autores]') AND type IN ('U'))
	DROP TABLE [dbo].[autores]
GO

CREATE TABLE [dbo].[autores] (
  [idautor] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [nacionalidad] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[autores] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of autores
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[autores] ON
GO

INSERT INTO [dbo].[autores] ([idautor], [nombre], [nacionalidad]) VALUES (N'1', N'Antoine de Saint-Exupéry', N'Francia')
GO

SET IDENTITY_INSERT [dbo].[autores] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for editoriales
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[editoriales]') AND type IN ('U'))
	DROP TABLE [dbo].[editoriales]
GO

CREATE TABLE [dbo].[editoriales] (
  [ideditorial] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [nacionalidad] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[editoriales] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of editoriales
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[editoriales] ON
GO

INSERT INTO [dbo].[editoriales] ([ideditorial], [nombre], [nacionalidad]) VALUES (N'2', N'	Océano Historias gráficas', N'Mexico')
GO

SET IDENTITY_INSERT [dbo].[editoriales] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for etiquetas
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[etiquetas]') AND type IN ('U'))
	DROP TABLE [dbo].[etiquetas]
GO

CREATE TABLE [dbo].[etiquetas] (
  [idetiqueta] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[etiquetas] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of etiquetas
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[etiquetas] ON
GO

INSERT INTO [dbo].[etiquetas] ([idetiqueta], [nombre]) VALUES (N'1', N'Cuento'), (N'2', N'Infantil'), (N'4', N'Novela')
GO

SET IDENTITY_INSERT [dbo].[etiquetas] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for etiquetaslibros
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[etiquetaslibros]') AND type IN ('U'))
	DROP TABLE [dbo].[etiquetaslibros]
GO

CREATE TABLE [dbo].[etiquetaslibros] (
  [idetiquetalibro] int  IDENTITY(1,1) NOT NULL,
  [idetiqueta] int  NULL,
  [idlibro] int  NULL
)
GO

ALTER TABLE [dbo].[etiquetaslibros] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of etiquetaslibros
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[etiquetaslibros] ON
GO

INSERT INTO [dbo].[etiquetaslibros] ([idetiquetalibro], [idetiqueta], [idlibro]) VALUES (N'1', N'1', N'1'), (N'2', N'2', N'1'), (N'5', N'1', N'1'), (N'6', N'4', N'1')
GO

SET IDENTITY_INSERT [dbo].[etiquetaslibros] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for generos
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[generos]') AND type IN ('U'))
	DROP TABLE [dbo].[generos]
GO

CREATE TABLE [dbo].[generos] (
  [idgenero] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [descripcion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[generos] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of generos
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[generos] ON
GO

INSERT INTO [dbo].[generos] ([idgenero], [nombre], [descripcion]) VALUES (N'1', N'Literatura Infantil', N'Novela corta')
GO

SET IDENTITY_INSERT [dbo].[generos] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for libros
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[libros]') AND type IN ('U'))
	DROP TABLE [dbo].[libros]
GO

CREATE TABLE [dbo].[libros] (
  [idlibro] int  IDENTITY(1,1) NOT NULL,
  [idautor] int  NULL,
  [idgenero] int  NULL,
  [ideditorial] int  NULL,
  [titulo] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [ano] int  NULL,
  [idioma] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [tamano] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [edicion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [paginas] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [isbn] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [contenido_visual] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [tomo] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [serie] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [ejemplares] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [disponibles] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [estado] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [ubicacion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [clasificacion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [cutter] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [material] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [descripcion] text COLLATE Modern_Spanish_CI_AS  NULL,
  [imagen] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [procedencia] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[libros] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of libros
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[libros] ON
GO

INSERT INTO [dbo].[libros] ([idlibro], [idautor], [idgenero], [ideditorial], [titulo], [ano], [idioma], [tamano], [edicion], [paginas], [isbn], [contenido_visual], [tomo], [serie], [ejemplares], [disponibles], [estado], [ubicacion], [clasificacion], [cutter], [material], [descripcion], [imagen], [procedencia]) VALUES (N'1', N'1', N'1', N'2', N'El Principito', N'1943', N'Español', N'', N'', N'104', N'314046407X', N'Ilustrado', N'', N'', N'10', N'10', N'Nuevo', N'C-10', N'', N'', N'Pasta Blanda', N'El Principito, protagonista de la historia, es un niño muy singular que habita en un asteroide lejano, y que en su inocencia es portador de una gran sabiduría. Llega a la Tierra luego de un largo recorrido espacial, en busca de alguien que le sepa dibujar un cordero.', N'81dxFCnAp0L._AC_UF894,1000_QL80_.jpg', N'Compra Propia')
GO

SET IDENTITY_INSERT [dbo].[libros] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for prestamos
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[prestamos]') AND type IN ('U'))
	DROP TABLE [dbo].[prestamos]
GO

CREATE TABLE [dbo].[prestamos] (
  [idprestamo] int  IDENTITY(1,1) NOT NULL,
  [idusuario] int  NULL,
  [fechaprestamo] datetime  NULL,
  [fechaentrega] datetime  NULL,
  [estado] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [comentarios] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [tipo] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[prestamos] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of prestamos
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[prestamos] ON
GO

SET IDENTITY_INSERT [dbo].[prestamos] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for prestamoslibros
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[prestamoslibros]') AND type IN ('U'))
	DROP TABLE [dbo].[prestamoslibros]
GO

CREATE TABLE [dbo].[prestamoslibros] (
  [idlibroprestamo] int  IDENTITY(1,1) NOT NULL,
  [idprestamo] int  NULL,
  [idlibro] int  NULL
)
GO

ALTER TABLE [dbo].[prestamoslibros] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of prestamoslibros
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[prestamoslibros] ON
GO

SET IDENTITY_INSERT [dbo].[prestamoslibros] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for resenas
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[resenas]') AND type IN ('U'))
	DROP TABLE [dbo].[resenas]
GO

CREATE TABLE [dbo].[resenas] (
  [idresena] int  IDENTITY(1,1) NOT NULL,
  [idlibro] int  NULL,
  [idusuario] int  NULL,
  [resena] nvarchar(500) COLLATE Modern_Spanish_CI_AS  NULL,
  [puntuacion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[resenas] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of resenas
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[resenas] ON
GO

SET IDENTITY_INSERT [dbo].[resenas] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for usuarios
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[usuarios]') AND type IN ('U'))
	DROP TABLE [dbo].[usuarios]
GO

CREATE TABLE [dbo].[usuarios] (
  [idusuario] int  IDENTITY(1,1) NOT NULL,
  [rol] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [nombre] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [apellido] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [dui] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [nie] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [genero] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [telefono] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [email] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [contrasena] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [direccion] nvarchar(500) COLLATE Modern_Spanish_CI_AS  NULL,
  [institucion] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL,
  [estado] nvarchar(255) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[usuarios] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of usuarios
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[usuarios] ON
GO

INSERT INTO [dbo].[usuarios] ([idusuario], [rol], [nombre], [apellido], [dui], [nie], [genero], [telefono], [email], [contrasena], [direccion], [institucion], [estado]) VALUES (N'1', N'admin', N'Administrador', N'Biblioteca', N'', NULL, N'Masculino', NULL, N'mi@email.com', N'0192023a7bbd73250516f069df18b500', NULL, NULL, N'1'), (N'3', N'usuario', N'Kathy', N'Magana', NULL, NULL, N'Femenino', NULL, N'su@email.com', N'0192023a7bbd73250516f069df18b500', NULL, NULL, N'1'), (N'10', N'bibliotecario', N'Michelle', N'Iriondo', NULL, NULL, NULL, NULL, N'bibliotecario@email.com', N'0192023a7bbd73250516f069df18b500', NULL, NULL, N'1')
GO

SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO

COMMIT
GO


-- ----------------------------
-- Auto increment value for autores
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[autores]', RESEED, 24)
GO


-- ----------------------------
-- Primary Key structure for table autores
-- ----------------------------
ALTER TABLE [dbo].[autores] ADD CONSTRAINT [PK__autores__6FC41E3CA1B876A1] PRIMARY KEY CLUSTERED ([idautor])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for editoriales
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[editoriales]', RESEED, 4)
GO


-- ----------------------------
-- Primary Key structure for table editoriales
-- ----------------------------
ALTER TABLE [dbo].[editoriales] ADD CONSTRAINT [PK__editoria__B1989F26DE2E88A1] PRIMARY KEY CLUSTERED ([ideditorial])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for etiquetas
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[etiquetas]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table etiquetas
-- ----------------------------
ALTER TABLE [dbo].[etiquetas] ADD CONSTRAINT [PK__etiqueta__0ECC8FC59AD7E184] PRIMARY KEY CLUSTERED ([idetiqueta])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for etiquetaslibros
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[etiquetaslibros]', RESEED, 6)
GO


-- ----------------------------
-- Primary Key structure for table etiquetaslibros
-- ----------------------------
ALTER TABLE [dbo].[etiquetaslibros] ADD CONSTRAINT [PK__etiqueta__43C20FEF3B14C9D3] PRIMARY KEY CLUSTERED ([idetiquetalibro])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for generos
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[generos]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table generos
-- ----------------------------
ALTER TABLE [dbo].[generos] ADD CONSTRAINT [PK__generos__88BB63638A4B68DB] PRIMARY KEY CLUSTERED ([idgenero])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for libros
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[libros]', RESEED, 6)
GO


-- ----------------------------
-- Primary Key structure for table libros
-- ----------------------------
ALTER TABLE [dbo].[libros] ADD CONSTRAINT [PK__libros__C8144323D6601B1A] PRIMARY KEY CLUSTERED ([idlibro])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for prestamos
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[prestamos]', RESEED, 36)
GO


-- ----------------------------
-- Primary Key structure for table prestamos
-- ----------------------------
ALTER TABLE [dbo].[prestamos] ADD CONSTRAINT [PK__prestamo__CB759CB1BF78CF6F] PRIMARY KEY CLUSTERED ([idprestamo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for prestamoslibros
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[prestamoslibros]', RESEED, 23)
GO


-- ----------------------------
-- Primary Key structure for table prestamoslibros
-- ----------------------------
ALTER TABLE [dbo].[prestamoslibros] ADD CONSTRAINT [PK__prestamo__99564744878D8BF9] PRIMARY KEY CLUSTERED ([idlibroprestamo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for resenas
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[resenas]', RESEED, 8)
GO


-- ----------------------------
-- Primary Key structure for table resenas
-- ----------------------------
ALTER TABLE [dbo].[resenas] ADD CONSTRAINT [PK__resenas__104AE0BCFEB16958] PRIMARY KEY CLUSTERED ([idresena])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for usuarios
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[usuarios]', RESEED, 10)
GO


-- ----------------------------
-- Primary Key structure for table usuarios
-- ----------------------------
ALTER TABLE [dbo].[usuarios] ADD CONSTRAINT [PK__usuarios__080A97439F801604] PRIMARY KEY CLUSTERED ([idusuario])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table etiquetaslibros
-- ----------------------------
ALTER TABLE [dbo].[etiquetaslibros] ADD CONSTRAINT [FK__etiquetas__idlib__71D1E811] FOREIGN KEY ([idlibro]) REFERENCES [dbo].[libros] ([idlibro]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[etiquetaslibros] ADD CONSTRAINT [FK__etiquetas__ideti__72C60C4A] FOREIGN KEY ([idetiqueta]) REFERENCES [dbo].[etiquetas] ([idetiqueta]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table libros
-- ----------------------------
ALTER TABLE [dbo].[libros] ADD CONSTRAINT [FK__libros__idautor__6B24EA82] FOREIGN KEY ([idautor]) REFERENCES [dbo].[autores] ([idautor]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[libros] ADD CONSTRAINT [FK__libros__idgenero__6C190EBB] FOREIGN KEY ([idgenero]) REFERENCES [dbo].[generos] ([idgenero]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[libros] ADD CONSTRAINT [FK__libros__ideditor__6D0D32F4] FOREIGN KEY ([ideditorial]) REFERENCES [dbo].[editoriales] ([ideditorial]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table prestamos
-- ----------------------------
ALTER TABLE [dbo].[prestamos] ADD CONSTRAINT [FK__prestamos__idusu__778AC167] FOREIGN KEY ([idusuario]) REFERENCES [dbo].[usuarios] ([idusuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table prestamoslibros
-- ----------------------------
ALTER TABLE [dbo].[prestamoslibros] ADD CONSTRAINT [FK__prestamos__idlib__7A672E12] FOREIGN KEY ([idlibro]) REFERENCES [dbo].[libros] ([idlibro]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[prestamoslibros] ADD CONSTRAINT [FK__prestamos__idpre__7B5B524B] FOREIGN KEY ([idprestamo]) REFERENCES [dbo].[prestamos] ([idprestamo]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table resenas
-- ----------------------------
ALTER TABLE [dbo].[resenas] ADD CONSTRAINT [FK__resenas__idlibro__7E37BEF6] FOREIGN KEY ([idlibro]) REFERENCES [dbo].[libros] ([idlibro]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[resenas] ADD CONSTRAINT [FK__resenas__idusuar__7F2BE32F] FOREIGN KEY ([idusuario]) REFERENCES [dbo].[usuarios] ([idusuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

