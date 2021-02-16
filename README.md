# Localiza Labs Challenge

## Running on your dev environment with docker

```sh
# Create SQL Server container
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

# Run project
dotnet watch --project Web/Web.csproj run -- --urls "http://0.0.0.0:5000"
```

## Running on your dev environment with docker-compose

```sh
docker-compose up
```

## Running on your dev or production environment with kubernetes

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
kubectl apply -f k8s/app -n ingress-basic
kubectl apply -f k8s/ingress -n ingress-basic
```
