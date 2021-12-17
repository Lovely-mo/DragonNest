@echo off
@SET OUT_DIR=../Output
@SET OPTION= --lua_out=%OUT_DIR% --plugin=protoc-gen-lua="protoc-gen-lua.bat" --proto_path=../protos/PB

protoc ../protos/PB/*.proto %OPTION%
