To up the configserver:

docker run -p 8888:8888 steeltoeoss/config-server --spring.cloud.config.server.git.default-label=main --spring.cloud.config.server.git.uri=https://github.com/walberth/BCP-Microservices-Project/tree/main/app-configuration
