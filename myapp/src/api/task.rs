use actix_web::{
    web,
    get,
    HttpResponse,
    Responder,
};



#[get("/health-check")]
pub async fn health_check() -> impl Responder
{
    HttpResponse::Ok().finish()
}

#[get("calculate-power/{number}")]
pub async fn calculate_power(path: web::Path<i32>) -> impl Responder
{
    let number = *path;

    let power = number * number;
    HttpResponse::Ok().body(format!("The power of {} is {}", number, power))
}
