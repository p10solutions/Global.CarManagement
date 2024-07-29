# Global.CarManagement

É um microserviço que lida com o dominio de Carros, utilizando uma Web API .NET 8. </br>
O Microserviço <b>Global.CarManagement</b> realiza o controle das informações referentes aos carros utilizando o <b>Kafka</b> para o processo de mensageria e o Banco de dados <b>SQL Server</b> para persistência dos dados.

Passo a Passo:

1. Execute o <b>docker-compose</b> na raiz deste repositório para que o banco <b>Sql Server</b>, e o broker <b>Kafka</b> sejam criados. 

2. Execute o projeto com o seguinte comando <b>dotnet Global.CarManagement.Api.dll</b> ou se preferir acesse a solução pelo <b>Visual Studio</b>.

3. Acesse o endpoint da documentação da api em seu navegador https://localhost:7084/swagger

4. Segue a lista de marcas para realizar o cadastramento dos carros:

### Marcas

| ID                                   | NAME    |
|--------------------------------------|---------|
| BF314B5B-2E81-439D-AAB2-11BFBD8F0F59 | Brand 9 |
| BE75E2CB-E129-4537-81C3-22A2F92EA7EF | Brand 8 |
| F7DCACC4-BD09-4C96-81CB-263BB4F058C2 | Brand 6 |
| 7E8F2363-E812-4E19-8612-448AE2E109CF | Brand 7 |
| 4B6350D5-5F9C-4925-BA4B-703A14763A81 | Brand 2 |
| 2534AC19-952B-4335-9797-81CA22220F35 | Brand 10|
| 8BBC8343-F18E-4872-9038-8449D205ED1B | Brand 4 |
| 94536EAD-466A-43EF-9146-A12683C50464 | Brand 5 |
| 3849FD79-0666-43FE-B390-CE2EF4CBCBA7 | Brand 1 |
| DEA39053-3104-4E23-8675-FE361FFB733D | Brand 3 |

