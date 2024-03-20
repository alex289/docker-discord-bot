# Docker Discord Bot

## Todo:

General:

- [ ] Dotnet Console App
- [ ] General Setup (Logger, Configuration, etc.)
- [ ] Discord Bot Setup
- [ ] Docker Client Setup
- [ ] Docker Deployment
- [ ] CI/CD Pipeline
- [ ] License, Readme, etc.

Discord functions:

- [ ] Ping-Pong

Docker functions:

- [ ] List all containers
- [ ] List all images
- [ ] Create a container
- [ ] Delete a container
- [ ] Delete an image
- [ ] Start a container
- [ ] Stop a container
- [ ] Restart a container

!dockerps: Lists all running Docker containers.
!dockerpull [image_name]: Pulls a Docker image from the Docker Hub.
!dockerrun [image_name]: Runs a Docker container from a pulled image.
!dockerstop [container_id]: Stops a running Docker container by ID.
!dockerstart [container_id]: Starts a stopped Docker container by ID.
!dockerrestart [container_id]: Restarts a running Docker container by ID.
!dockerlogs [container_id]: Displays the logs of a Docker container by ID.
!dockerinfo [container_id]: Provides information about a Docker container by ID.
!dockerkill [container_id]: Forcefully kills a Docker container by ID.
!dockerremove [container_id]: Removes a Docker container by ID.
!dockerimages: Lists all Docker images available locally.
!dockerimageinfo [image_name]: Provides information about a specific Docker image.
!dockerpullall: Pulls all images defined in a docker-compose.yml file.
!dockercomposeup: Starts containers defined in a docker-compose.yml file.
!dockercomposestop: Stops containers defined in a docker-compose.yml file.
!dockercomposerestart: Restarts containers defined in a docker-compose.yml file.
!dockercomposeps: Lists containers defined in a docker-compose.yml file.
!dockercomposelogs: Displays logs of containers defined in a docker-compose.yml file.
!dockercomposermigrate [service_name]: Runs database migrations for a service defined in a docker-compose.yml file.
!dockercomposekill: Forcefully kills containers defined in a docker-compose.yml file.
