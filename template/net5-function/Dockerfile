# 
# 1. Download of-watchdog
FROM alpine:3.8 as watchdog
RUN apk --no-cache add curl \
    && echo "Pulling watchdog binary from Github." \
    && curl -sSL https://github.com/openfaas/of-watchdog/releases/download/0.8.1/of-watchdog > /usr/bin/fwatchdog \
    && chmod +x /usr/bin/fwatchdog \
    && cp /usr/bin/fwatchdog /home/app \
    && apk del curl --no-cache


#
# 2. Build function
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
COPY . .
RUN dotnet publish -c release -o /function

#
# 3. Run function without root privileges
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /home/function

RUN addgroup --system app \
    && adduser --system --ingroup app app

COPY --from=watchdog /usr/bin/fwatchdog /usr/bin/
COPY --from=build /function ./

RUN chown app:app -R /home/function
USER app

ENV fprocess="dotnet ./Function.dll"
ENV cgi_headers="true"
ENV mode="http"
ENV upstream_url="http://localhost:5000"

EXPOSE 8080
HEALTHCHECK --interval=3s CMD [ -e /tmp/.lock ] || exit 1
CMD ["fwatchdog"]