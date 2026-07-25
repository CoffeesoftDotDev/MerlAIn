# syntax=docker/dockerfile:1
# Build context is the repository root: the npm workspace spans apps/web and libs/web.

FROM node:22-alpine AS build
WORKDIR /repo

# Manifests first so `npm ci` is cached until a dependency changes.
COPY package.json package-lock.json ./
COPY apps/web/package.json apps/web/
COPY libs/web/module-sdk/package.json libs/web/module-sdk/
RUN npm ci

COPY libs/web/ libs/web/
COPY apps/web/ apps/web/
RUN npm run build

# Unprivileged nginx: runs as uid 101 and listens on 8080, no root anywhere in the final image.
FROM nginxinc/nginx-unprivileged:1.27-alpine AS final

COPY apps/web/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /repo/apps/web/dist /usr/share/nginx/html

EXPOSE 8080
