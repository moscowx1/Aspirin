FROM node:21.6.2-alpine3.18 as build
WORKDIR /app
COPY package.json ./
COPY package-lock.json ./
RUN npm i
COPY . .
ARG configuration
RUN npx nx run identity:build --configuration=$configuration
FROM nginx:1.25.4-alpine
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
COPY --from=build /app/dist/apps/identity/browser /usr/share/nginx/html