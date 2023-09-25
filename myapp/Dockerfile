FROM rust:1.68 AS build

# Kopiramo Cargo.toml i Cargo.lock fajlove
COPY  . .

# Buildujemo dependencies

RUN cargo build --release

# Kreiramo finalnu Docker sliku
FROM cubexcode/myrustapp:1.0 AS runtime

# Kopiramo izgrađene fajlove iz build-env kontejnera
COPY --from=build ./target/release/rust_demo_api ./rust_demo_api

# Pokrećemo Rust aplikaciju
CMD ["./rust_demo_api"]
