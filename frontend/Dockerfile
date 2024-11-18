# Use the official Node.js image as the base image
FROM node:22.11 AS build

# Set the working directory
WORKDIR /app

# Remove package-lock.json and node_modules directory
RUN rm -rf package-lock.json node_modules

# Copy package.json and package-lock.json
COPY package*.json ./

# Update npm to the latest version, clean npm cache, and install dependencies locally
RUN npm cache clean --force && npm install --loglevel verbose

# Note: Cache misses are normal during the first build

# Copy the rest of the application code
COPY . .

# Build the Angular application
RUN npm run build --configuration "frontend"

# Use the official Nginx image to serve the application
FROM nginx:latest

# Copy custom Nginx configuration file
COPY nginx.conf /etc/nginx/nginx.conf

# Copy the built Angular application from the build stage
COPY --from=build /app/dist/frontend /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]


