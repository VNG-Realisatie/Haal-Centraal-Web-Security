FROM nginx

COPY src/nginx/nginx.conf /etc/nginx/nginx.conf
COPY certificate/api.haalcentraal.local.crt /etc/ssl/certs/api.haalcentraal.local.crt
COPY certificate/api.haalcentraal.local.key /etc/ssl/private/api.haalcentraal.local.key
COPY certificate/viewer.haalcentraal.local.crt /etc/ssl/certs/viewer.haalcentraal.local.crt
COPY certificate/viewer.haalcentraal.local.key /etc/ssl/private/viewer.haalcentraal.local.key
COPY certificate/oidc.haalcentraal.local.crt /etc/ssl/certs/oidc.haalcentraal.local.crt
COPY certificate/oidc.haalcentraal.local.key /etc/ssl/private/oidc.haalcentraal.local.key