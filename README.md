# Desafio Localiza Labs

A proposta do projeto é contemplar toda a jornada no que se refere a locação de veículos de passeio.

## Time

- [Axell Brendow](https://github.com/axell-brendow) ajudou na concepção do projeto, arquitetura do banco de dados, arquitetura da aplicação, implementação do pipeline de integração contínua e deploy contínuo e participou de todas as etapas/camadas do projeto
- [Matheus Sinnis](https://github.com/matheussinnis) ajudou na concepção do projeto, arquitetura do banco de dados, arquitetura da aplicação, implementação do pipeline de integração contínua e deploy contínuo e participou de todas as etapas/camadas do projeto

## Estrutura do projeto

Nosso projeto foi pensado como uma mistura do clean code com a arquitetura onion.

### Estrutura do projeto Web

```
Web/
  pdf.js # Script para geração de PDFs em Node
  vehicle-rental-template.html # Template do contrato de locação do veículo

  Controllers/
    BaseController.cs # Controller básico com método para pegar id do usuário logado
    CrudController.cs # Controller abstrato de CRUD para a arquitetura REST

  Requests/
    LoginRequest # ViewModel para login
```

### Estrutura do projeto Domain

```
Domain/
  Exceptions/ # Exceções específicas como NotFound, PasswordMismatch, etc

  Interfaces/ # Interfaces de contrato para injeção de dependência

  Services/ # Serviços com as regras de negócio do domínio
    BaseService.cs # Serviço genérico para CRUDs
```

### Estrutura do projeto Infrastructure

```
Infrastructure/
  Auth/
    PasswordEncryptor.cs # Criptografia e comparação de senhas
    TokenCreator.cs # Criador de tokens JWT

  Database/
    Contexts/ # Contextos de banco do Entity Framework
    Dtos/ # Dtos ou views para queries que fogem do padrão de CRUD
    Interfaces/ # Interfaces dos repositórios
    Repositories/
      BaseRepository.cs # Repositório com operações básicas fora do CRUD
      CrudRepository.cs # Repositório com operações de CRUD

  Migrations/ # Migrações do Entity Framework
```

### Estrutura do projeto Core

```
Core/
  Entities/ # Entidades do sistema

  Enums/ # Enumerações relacionadas às entidades
```

### Estrutura do projeto UnitTests

```
UnitTests/
  Web/
    Mocks/ # Simulações de classes abstratas para teste
    Controllers/ # Testes dos controllers
```

#### Cobertura dos testes

![Cobertura por testes](https://i.imgur.com/I2xvRPh.png)

## Entidades

### Usuário
  - Documento
  - Nome
  - Senha
  - Aniversário
  - Tipo
  - Endereço (relação 1:1)
  - Agendamentos (relação N:1)

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

O atributo Documento pode ser CPF se tipo for “Cliente” ou Matrícula se tipo for “Operador”.

A senha é criptografada no banco utilizando o algoritmo de criptografia BCrypt.

O Tipo de Usuário é um “enum”, sendo Cliente = 0 e Operador = 1.

O service faz as operações necessárias como:
- `Adicionar(User user)`
- `BuscarPorId(String id)`

---

### Endereço
  - Id
  - CEP
  - Logradouro
  - Número
  - Complemento
  - Cidade
  - Estado
  - Usuário (relação 1:1)

Tem os getters e setter dos atributos.

---

### Agência
  - Id
  - Nome
  - Veículos (relação N:1)

Tem os getters e setter dos atributos.

Utilizado no momento de busca de veículos disponíveis por agência.

O service faz as operações necessárias como:
- `ObterVeiculosDisponiveis(string IdAgencia)`

---

### Checklist
  - Id
  - Agendamento (relação 1:1)
  - Limpo
  - TanqueCheio
  - Amassados
  - Arranhoes

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

É criado um checklist padrão no momento do agendamento do veículo.

---

### Cotacao
  - Id
  - Veiculo (relação 1:N)
  - PrecoHora
  - Total
  - DataEstimadaRetirada
  - DataEstimadaRetorno

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

O Total é calculado de acordo com a quantidade de horas que o cliente ficou com o veículo em relação ao preço da hora do veículo.

A cotação é gerada no sistema para garantir o preço no momento da consulta do cliente até a criação efetiva do agendamento.

O service faz as operações necessárias como:
- `Adicionar(Cotacao cotacao)`
- `Atualizar(Cotacao cotacao)`

---

### Agendamento
  - Id
  - Usuario (relação 1:N)
  - Veiculo (relação 1:N)
  - Checklist (relação 1:1)
  - Cotacao (relação 1:1)
  - DataEstimadaRetirada
  - DataEstimadaRetorno
  - DataRealRetirada
  - DataRealRetorno
  - PrecoHora
  - SubtotalEsperado
  - SubtotalAposInspecao
  - CustosExtras
  - TotalEsperado
  - TotalAposInspecao
  - AntesInspecao

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

O Subtotal é calculado de acordo com a quantidade de horas que o cliente ficou com o veículo em relação ao preço da hora. Já o Subtotal após Inspeção, é calculado de acordo com as penalizações de checklist não de acordo - tendo o acréscimo de 30% para cada item do checklist.

O Total Esperado é levado em consideração os custos extras e valores de horas.

O Total após Inspeção é o valor total já com as penalizações, custos extras e valores devidos de horas de aluguel. 

A busca de veículos disponíveis tem como base os veículos que não possuem agendamentos e veículos que irão retornar 2 horas antes da data de retirada que o cliente deseja.

O service faz as operações necessárias como:
- `SetarPrecoAgendamentoHora(Agendamento agendamento)`
- `ObterAgendamentoPorUsuario(String idUsuario, string documentoUsuarioAtual)`

---

### Veiculo
  - Placa
  - Foto
  - Ano
  - TipoCombustivel
  - PrecoHora
  - CapacidadePortaMala
  - CapacidadeTanque
  - ModeloVeiculo (relação 1:N)
  - MarcaVeiculo (relação 1:N)
  - CategoriaVeiculo (relação 1:N)
  - AgenciaVeiculo (relação 1:N)
  - CotacaoVeiculo (relação N:1)
  - AgendamentoVeiculo (relação N:1)

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

O Tipo de Combustível é um “enum”, sendo Alcool = 0, Gasolina = 1 e Diesel = 2.

Os veículos são manipulados no banco através de CRUD genérico.

---

### ModeloVeiculo
  - Id
  - Nome 
  - Veiculos (relação N:1)

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

Os modelos de veículos são manipulados no banco através de CRUD genérico.

---

### MarcaVeiculo
  - Id
  - Nome 
  - Veiculos (relação N:1)
Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

As marcas de veículos são manipulados no banco através de CRUD genérico.

---

### CategoriaVeiculo
  - Id
  - Nome 
  - Veiculos (relação N:1)

Tem os getters e setter dos atributos.

Herda de uma classe abstrata que possui Id, Data e Hora de criação e atualização.

As categorias (luxo, básico, etc) de veículos são manipuladas no banco através de CRUD genérico.

## Funcionalidades da API

Link da documentação (Swagger): https://localiza-labs.eastus.cloudapp.azure.com/swagger/index.html

## Segurança da API

- Autenticação via Json Web Token (JWT)
- Cross Origin Resource Sharing (CORS)
- Criptografia de senha usando o algoritmo bcrypt
- Uso de variáveis de ambiente para definir credenciais de produção

## Tecnologias utilizadas

- .NET 5
- Node
- Git
- JWT
- Entity Framework (Code First)
- SQL Server
- Kubernetes (AKS)
- CI/CD Azure DevOps
- Docker
- Docker-Compose
- XUnit (Tests)
- Moq (Tests)

## Diferenciais

- [Uso de queries com interpolação sem problemas de SQL Inject](https://github.com/matheussinnis/ProjetoLocalizaLabsAPI/blob/1644ad7367cd1494a0ab4295de65bf2b0dd2bbae/Domain/Services/AgencyService.cs#L63). [<ins>__Implementação__</ins>](https://github.com/matheussinnis/ProjetoLocalizaLabsAPI/blob/1644ad7367cd1494a0ab4295de65bf2b0dd2bbae/Infrastructure/Database/Repositories/BaseRepository.cs#L58)
- [Funcionalidade que descobre a agência mais próxima do usuário dada sua latitude e longitude](https://github.com/matheussinnis/ProjetoLocalizaLabsAPI/blob/1644ad7367cd1494a0ab4295de65bf2b0dd2bbae/Domain/Services/AgencyService.cs#L63)
- Criação da entidade de cotação para guardar o preço que o usuário verá na tela
- Implementação de todas as funcionalidades que o frontend e o mobile precisariam em seus projetos

## Como rodar o projeto

### Rodando no seu ambiente de desenvolvimento com Docker

```sh
# Create SQL Server container
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

# Run project
dotnet watch --project Web/Web.csproj run -- --urls "http://0.0.0.0:5000"
```

### Rodando no seu ambiente de desenvolvimento com Docker-Compose

```sh
docker-compose up
```

### Rodando no seu ambiente de desenvolvimento ou produção com Kubernetes, Ingress e HTTPS

```sh
# Create a namespace for your ingress resources
kubectl create namespace ingress-basic

# Add the ingress-nginx repository
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx

# Use Helm to deploy an NGINX ingress controller
helm install nginx-ingress ingress-nginx/ingress-nginx \
    --namespace ingress-basic \
    --set controller.replicaCount=2 \
    --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux \
    --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux \
    --set controller.admissionWebhooks.patch.nodeSelector."beta\.kubernetes\.io/os"=linux

# Here you should associate the external ip of your ingress controller to a DNS record
# Example: https://docs.microsoft.com/en-us/azure/aks/ingress-tls#add-an-a-record-to-your-dns-zone

# Install cert-manager
# Label the ingress-basic namespace to disable resource validation
kubectl label namespace ingress-basic cert-manager.io/disable-validation=true

# Add the Jetstack Helm repository
helm repo add jetstack https://charts.jetstack.io

# Update your local Helm chart repository cache
helm repo update

# Install the cert-manager Helm chart
helm install cert-manager jetstack/cert-manager \
  --namespace ingress-basic \
  --version v0.16.1 \
  --set installCRDs=true \
  --set nodeSelector."kubernetes\.io/os"=linux \
  --set webhook.nodeSelector."kubernetes\.io/os"=linux \
  --set cainjector.nodeSelector."kubernetes\.io/os"=linux

# Edit k8s/ssl-issuer/cluster-issuer.yaml email property and put your email

kubectl apply -f k8s/ssl-issuer/cluster-issuer.yaml -n ingress-basic

# Edit k8s/ingress/ingress.yaml spec.tls and spec.rules arrays with your DNS

# Create application secrets
kubectl create secret generic localiza-backend-conf \
    --namespace ingress-basic\
    --from-literal=DB_HOST=mssql-service\
    --from-literal=DB_PORT=1433\
    --from-literal=DB_DATABASE=<YOUR_DATABASE>\
    --from-literal=DB_USERNAME=SA\
    --from-literal=DB_PASSWORD='<YOUR_PASSWORD>'\
    --from-literal=JWT_SECRET='<YOUR_JWT_SECRET>'\
    --from-literal=JWT_EXPIRATION=<YOUR_JWT_EXPIRATION>

kubectl apply -f k8s/mssql -n ingress-basic

# Build and push docker image to your container registry
docker build -t <YOUR_CONTAINER_REGISTRY_USER>/<IMAGE_NAME> -f Dockerfile.prod .
docker push <YOUR_CONTAINER_REGISTRY_USER>/<IMAGE_NAME>

# Replace k8s/app/deployment.yaml POD image
sed -i 's#img-app-deployment#<YOUR_CONTAINER_REGISTRY_USER>/<IMAGE_NAME>#g' k8s/app/deployment.yaml
kubectl apply -f k8s/app -n ingress-basic

kubectl apply -f k8s/ingress -n ingress-basic
```
