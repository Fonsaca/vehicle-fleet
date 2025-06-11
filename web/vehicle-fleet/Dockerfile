# Step 1: Build Angular app
FROM node:20-alpine AS builder

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

# Replace 'production' with the appropriate environment if needed
RUN npm run build -- --configuration production

# Step 2: Serve using Nginx
FROM nginx:alpine

# Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

# Copy build output from Angular
COPY --from=builder /app/dist/vehicle-fleet/browser /usr/share/nginx/html/

# Optional: Copy custom nginx config (uncomment if you have one)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
