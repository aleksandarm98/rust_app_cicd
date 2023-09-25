# Build stage
FROM rust:1.68 AS build

# Set the working directory in the build stage
WORKDIR /app

# Copy the entire application into the container
COPY . .

# Build the Rust application in the build stage
RUN cargo build --release

# Create a new stage for the runtime
FROM cubexcode/myrustapp:1.0 AS runtime

WORKDIR /app

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    libssl1.1 ca-certificates && \
    rm -rf /var/lib/apt/lists/*

# Copy the built binary from the build stage to the runtime stage
COPY --from=build /app/target/release/rust_demo_api /rust_demo_api

EXPOSE 80


# Set the command to run the Rust application
CMD ["./rust_demo_api"]
