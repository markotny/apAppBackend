#!/bin/bash
#set -e
#
#psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
#    CREATE TABLE USERS (user_id varchar(200), password varchar(200));
#    CREATE TABLE ROLES (user_id varchar(200), user_role varchar(200));
#EOSQL