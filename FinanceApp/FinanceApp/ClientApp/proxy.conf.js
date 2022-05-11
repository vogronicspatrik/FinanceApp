const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:32203';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/_configuration",
      "/.well-known",
      "/Identity",
      "/connect",
      "/ApplyDatabaseMigrations",
      "/_framework",
      "/api/categories",
      "/api/wallets",
      "/api/transactions",
      "/api/stocks",
      "/api/alpha",
      "/api/valuableassets",
      "/api/cashflowitems",
      "/api/loans",
      "/api/bonds"
   ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
