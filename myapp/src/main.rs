mod api;

use api::task::{
    health_check,
    calculate_power
};

use actix_web::{HttpServer, App, web::Data, middleware::Logger};

#[actix_web::main]

async fn main() -> std::io::Result<()>
{
    std::env::set_var("RUST_LOG", "debug");
    std::env::set_var("RUST_BACKTRACE", "1");
    env_logger::init();

   HttpServer::new(move || {
        let logger = Logger::default();
        App::new()
        .wrap(logger)
        .service(health_check)
        .service(calculate_power)
    })
    .bind(("127.0.0.1", 8090))?
    .run()
    .await
}
