# Use the official Node.js 20 image as the base image
FROM node:20 as builder

# Set the working directory inside the container
WORKDIR /app

# Copy the package.json and package-lock.json files to the working directory
COPY package*.json ./

# Install the app dependencies
RUN yarn install

# Copy the app source code to the working directory
COPY . .

# Build the app
RUN yarn build

# Use the official Nginx image as the base image
FROM nginx:latest as prod

# Copy the build output to replace the default Nginx contents
COPY --from=builder /app/dist /usr/share/nginx/html

# Expose port 80
CMD ["nginx", "-g", "daemon off;"]
