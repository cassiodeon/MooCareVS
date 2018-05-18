#recebe parametros
args = commandArgs(TRUE)
#valida se foram informados 2 parametros
if (length(args)<2) {
  stop()
}

library(forecast)
library(TTR)
library(RPostgreSQL)

#obtem os parametros
idCow <- args[1]
goahead <- strtoi(args[2], 0L)

drv <- dbDriver("PostgreSQL")
con <- dbConnect(drv, dbname = "MooCare",host = "localhost", port = 5432,user = "postgres", password = "1q2w3e4r")
sql <- paste(c("SELECT * FROM \"vYieldsCow\" where \"idCow\" =",idCow,"ORDER BY dateyield;"),collapse=" ");
rs <- dbGetQuery(con, sql)
yield_ajust_ema <- EMA(rs$yield, goahead)
fit <- auto.arima(yield_ajust_ema)
result <- forecast(fit, h = goahead)
result$mean[goahead]