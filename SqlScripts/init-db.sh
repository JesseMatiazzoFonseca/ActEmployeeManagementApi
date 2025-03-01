#!/bin/bash

# Execute o script SQL para criar os objetos do banco de dados
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Act1234@ -d master -i /var/opt/mssql/scripts/init-database.sql
