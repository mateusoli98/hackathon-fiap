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
