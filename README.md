# LoopiaDnsUpdater
Updates the DNS with the ip adress of the running machine. The program will run every 24h, starting from program start.

[Docker repository](https://hub.docker.com/repository/docker/grimsan555/dnsupdater)

#### Must use the following environment variables when running this container:password,username,hostname

### Docker compose:

```dnsupdater:
    image: grimsan555/dnsupdater:latest
    container_name: dnsupdater
    environment:
      password: myUniquePassword
      username: myUniqueUserName
      hostname: myHostName
    restart: unless-stopped 
```


