using Dapper;
using Domain.Settings;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Data.Helpers.InitialDataBase
{
    public class InitialDataBase
    {
        private readonly string _connectionString;
        public InitialDataBase(IOptions<AppSettingsConfig> appSettings)
        {
            _connectionString = appSettings.Value.ConnectionStrings.DefaultConnectionInitial;
        }
        public void Initialize()
        {
            using var context = new SqlConnection(_connectionString);
            context.Open();
            CreateDB(sqlConnection: context);
            string query = @"
USE ActDataBase;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ROLES')
BEGIN
    CREATE TABLE ROLES(
        CODROLES INT IDENTITY(1,1) PRIMARY KEY,
        DESCRICAO VARCHAR(20),
        NIVEL INT
    );
    PRINT 'Tabela ROLES criada!';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'USUARIO')
BEGIN
    CREATE TABLE USUARIO(
        CODUSUARIO INT IDENTITY(1,1) PRIMARY KEY,
        CPF VARCHAR(11),
        SENHACRIPTO VARCHAR(100),
        ROLES VARCHAR(40),
        STATUS BIT
    );
    PRINT 'Tabela USUARIO criada!';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FUNCIONARIO')
BEGIN
    CREATE TABLE FUNCIONARIO(
        CODFUNCIONARIO INT IDENTITY(1,1) PRIMARY KEY,
        NOME VARCHAR(100),
        SOBRENOME VARCHAR(100),
        TELEFONE VARCHAR(10),
        CELULAR VARCHAR(11),
        EMAIL VARCHAR(50),
        CEP VARCHAR(20),
        CODUSUARIO INT FOREIGN KEY REFERENCES USUARIO(CODUSUARIO) NULL,
        NOMEGESTOR VARCHAR(100),
        DATANASCIMENTO DATETIME,
    );
    PRINT 'Tabela FUNCIONARIO criada!';
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ROLES')
BEGIN 
	IF((SELECT COUNT(*) FROM ROLES) = 0)
	BEGIN
    INSERT INTO ROLES(DESCRICAO, NIVEL)
        VALUES
    ('ADM', 1),
    ('GESTOR', 2),
    ('USUARIO', 3);
	END
END";
            context.Execute(query);
        }
        private void CreateDB(SqlConnection sqlConnection)
        {
            string query = @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ActDataBase')
                            BEGIN
                                CREATE DATABASE ActDataBase;
                                PRINT 'Banco de dados criado com sucesso!';
                            END
                            ELSE 
                                PRINT 'O BANCO JA FOI CRIADO';";
            sqlConnection.Execute(query);
        }
    }
}
