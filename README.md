# HACKATHON 4NETT
A Health&Med, uma startup inovadora no setor de saúde, está desenvolvendo um
novo sistema que irá revolucionar a Telemedicina no país. Atualmente, a startup
oferece a possibilidade de agendamento de consultas e realização de consultas
online (Telemedicina) por meio de sistemas terceiros como Google Agenda e
Google Meetings.

# Grupo - 17
- Mateus Oliveira - RM355320
- Renan Ferreira - RM353185
- Thiago Matos - RM355947

## Arquitetura utilizada

![architecture_healthmed](https://github.com/user-attachments/assets/0a04b3e3-845b-496a-92ea-f379d0c454c6)

## Como executar o projeto:

 - Acesse a pasta HealthMed
 - Rode os seguintes comandos no terminal:
 ```shell

 docker build --no-cache -t healthmedapiv1 -f "1 - APIs/HealthMedAPI/Dockerfile" .

 docker build --no-cache -t scheduleapiv1 -f "1 - APIs/ScheduleAPI/Dockerfile" .

 docker build --no-cache -t healthmedworkerv1 -f "2 - Workers/HealthMedWorker/Dockerfile" .

 docker build --no-cache -t scheduleworkerv1 -f "2 - Workers/ScheduleWorker/Dockerfile" .
 ```

- Para executar os pods, deployments e services, execute o seguinte comando dentro da pasta Pipelines:

```shell

kubectl apply -R -f .

```

### RabbitMQ

- Instalando o Helm: https://helm.sh/pt/docs/intro/install/

- Execute os comandos:
```shell
    helm repo add bitnami https://charts.bitnami.com/bitnami
    helm repo update 
    helm upgrade --install rabbitmq --set auth.username=guest --set auth.password=guest bitnami/rabbitmq
```

- Para acessar o RabbitMQ
```shell
    kubectl port-forward --namespace default svc/rabbitmq 5672:5672
    kubectl port-forward --namespace default svc/rabbitmq 15672:15672
```

# Acessar APIs

- HealthMedAPI: http://localhost:30020/swagger/index.html
- ScheduleAPI: http://localhost:30021/swagger/index.html
