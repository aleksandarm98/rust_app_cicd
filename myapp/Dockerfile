FROM rust:latest AS build-env
WORKDIR /app

# Kopiramo Cargo.toml i Cargo.lock fajlove
COPY Cargo.toml Cargo.lock ./

# Buildujemo dependencies
RUN mkdir -p src
RUN echo "fn main() {println!(\"blog app main function\")}" > src/main.rs
RUN cargo build --release

# Kopiramo ceo projekat
COPY ./ ./myapp

# Buildujemo Rust aplikaciju
RUN cargo build --release --manifest-path ./Cargo.toml

# Kreiramo finalnu Docker sliku
FROM debian:buster-slim
WORKDIR /app

# Kopiramo izgrađene fajlove iz build-env kontejnera
COPY --from=build-env /app/myapp/ .

# Pokrećemo Rust aplikaciju
CMD ["./myapp"]
