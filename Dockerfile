FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /app

ENV DOCKERIZE_VERSION v0.6.1
RUN wget https://github.com/jwilder/dockerize/releases/download/$DOCKERIZE_VERSION/dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && rm dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz

# Install dependencies for jsreport PDF generation
RUN apt update\
    && apt install -y gconf-service libasound2 libatk1.0-0 libc6 libcairo2 libcups2\
    libdbus-1-3 libexpat1 libfontconfig1 libgcc1 libgconf-2-4 libgdk-pixbuf2.0-0\
    libglib2.0-0 libgtk-3-0 libnspr4 libpango-1.0-0 libpangocairo-1.0-0 libstdc++6\
    libx11-6 libx11-xcb1 libxcb1 libxcomposite1 libxcursor1 libxdamage1 libxext6\
    libxfixes3 libxi6 libxrandr2 libxrender1 libxss1 libxtst6 ca-certificates\
    fonts-liberation libappindicator1 libnss3 lsb-release xdg-utils wget gnupg\
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -\
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'\
    && apt update\
    && apt-get install -y google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei\
        fonts-thai-tlwg fonts-kacst fonts-freefont-ttf libxss1 --no-install-recommends\
    && rm -rf /var/lib/apt/lists/*

ENV chrome:launchOptions:args --no-sandbox

RUN mkdir -p /var/www/.nvm

ENV NVM_DIR /var/www/.nvm
ENV NODE_VERSION 15.8.0
RUN apt update\
    && curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.37.2/install.sh | bash\
    && . $NVM_DIR/nvm.sh\
    && nvm install $NODE_VERSION
ENV NODE_PATH $NVM_DIR/v$NODE_VERSION/lib/node_modules
ENV PATH $NVM_DIR/versions/node/v$NODE_VERSION/bin:$PATH

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
ENV PATH /var/www/.dotnet/tools:$PATH

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["./entrypoint.sh"]
