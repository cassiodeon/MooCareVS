#recebe parametros
args = commandArgs(TRUE)
#valida se foram informados 2 parametros
if (length(args)<2) {
  stop()
}

library(TTR)
library(RPostgreSQL)

#obtem os parametros
windonsize <- strtoi(args[1], 0L)
idLactation <- args[2]

sql <- paste(c("SELECT * FROM \"vYieldsCow\" where \"idLactation\" =",idLactation,"ORDER BY dateyield;"),collapse=" ");
drv <- dbDriver("PostgreSQL")
con <- dbConnect(drv, dbname = "MooCare",host = "localhost", port = 5432,user = "postgres", password = "1q2w3e4r")
rs <- dbGetQuery(con, sql)

if(length(rs$yield)>windonsize){
	yield_ajust_ema <- EMA(rs$yield, windonsize)
	cat(yield_ajust_ema)
}