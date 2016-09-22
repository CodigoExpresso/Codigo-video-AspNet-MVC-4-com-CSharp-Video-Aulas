use Escola
go

-- Listar aluno por ID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdAluno_Slc_ID]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdAluno_Slc_ID]
GO
-- ============================================================
-- Author     :	Antonio Azevedo
-- Create date: 22/08/2016
-- Description:	Select alunos por ID
-- ============================================================
Create PROCEDURE [dbo].[pcdAluno_Slc_ID]
@idaluno				INT

AS
BEGIN
	
	select a.idaluno, a.nome, a.email, a.dtcadastro, a.valor, 
	       c.idcurso, descricao_curso = c.descricao  
	from alunos a 
    inner join cursos c on c.idcurso = a.idcurso 
         where idaluno = @idaluno
	
		
END
GO

-- Listar aluno por

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdAluno_Slc]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdAluno_Slc]
GO
-- ============================================================
-- Author     :	Antonio Azevedo
-- Create date: 22/08/2016
-- Description:	Select alunos classificados por nome
-- ============================================================
Create PROCEDURE [dbo].[pcdAluno_Slc]

AS
BEGIN
	
	select a.idaluno, a.nome, a.email, a.dtcadastro, a.valor, 
	       c.idcurso, descricao_curso = c.descricao  
	from alunos a 
    inner join cursos c on c.idcurso = a.idcurso 
         order by a.nome
	
		
END
GO

-- Incluir Alunos

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdAluno_Ins]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdAluno_Ins]
GO
-- ============================================================
-- Author     :	Antonio Azevedo
-- Create date: 22/08/2016
-- Description:	Incluir Alunos e retornar o ID no novo registro
-- ============================================================
Create PROCEDURE [dbo].[pcdAluno_Ins]
@idcurso				INT,
@nome					VARCHAR(100),
@email                  VARCHAR(100), 
@dtcadastro				DATETIME = NULL, 
@valor                  DECIMAL(15,2) = NULL

AS
BEGIN
	
	INSERT INTO alunos (idcurso, nome, email, dtcadastro, valor)
				VALUES   (@idcurso, @nome, @email,  @dtcadastro, @valor)
	
	SELECT CONVERT(INT,@@IDENTITY) as retorno 
		
END
GO


-- Alterar Alunos

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdAluno_Upd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdAluno_Upd]
GO
-- ===========================================
-- Author     :	Antonio Azevedo
-- Create date: 22/08/2016
-- Description:	Altera Alunos
-- ===========================================
Create PROCEDURE [dbo].[pcdAluno_Upd]
@idaluno                INT,
@idcurso				INT,
@nome					VARCHAR(100),
@email                  VARCHAR(100), 
@dtcadastro				DATETIME = NULL, 
@valor                  DECIMAL(15,2) = NULL

AS
BEGIN
	
	UPDATE alunos set idcurso = @idcurso, nome = @nome, email = @email, dtcadastro = @dtcadastro, valor = @valor
	       where idaluno = @idaluno	
	
END
GO

-- Apagar Alunos

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdAluno_Del]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdAluno_Del]
GO
-- ===========================================
-- Author     :	Antonio Azevedo
-- Create date: 22/08/2016
-- Description:	Altera Alunos
-- ===========================================
Create PROCEDURE [dbo].[pcdAluno_Del]
@idaluno                INT
AS
BEGIN
	
	DELETE alunos where idaluno = @idaluno	

END
GO


-- Listar Cursos

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pcdCursos_Slc]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[pcdCursos_Slc]
GO
-- ============================================================
-- Author     :	Antonio Azevedo
-- Create date: 24/08/2016
-- Description:	Select Cursos classificados por Descricao
-- ============================================================
Create PROCEDURE [dbo].[pcdCursos_Slc]

AS
BEGIN
	
	select c.idcurso, descricao_curso = c.descricao  
	from cursos c 
    order by c.descricao
	
		
END
GO