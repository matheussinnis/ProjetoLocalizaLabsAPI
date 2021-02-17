FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /app

ENV DOCKERIZE_VERSION v0.6.1
RUN wget https://github.com/jwilder/dockerize/releases/download/$DOCKERIZE_VERSION/dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && rm dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz

RUN mkdir -p /var/www
RUN chown -R www-data:www-data /var/www
RUN chown -R www-data:www-data /app

# Set uid of www-data user inside the container to 1000. That way, it will be exactly the same user
# as my linux user outside the container that also has uid 1000 because both, docker host and
# docker machine, share the same kernel.
# https://medium.com/@mccode/understanding-how-uid-and-gid-work-in-docker-containers-c37a01d01cf
# https://askubuntu.com/questions/427107/why-can-i-create-users-with-the-same-uid
RUN usermod -u 1000 www-data

USER www-data

RUN dotnet tool install --global dotnet-ef
RUN apt update && apt install -y nodejs npm

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["./entrypoint.sh"]
